using BenchmarkViewer.Models.Contracts;
using BenchmarkViewer.Models.Database;
using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BenchmarkViewer.Services
{
    public class DataStorageService
    {
        const string ConnectionString = @"Server=.;Integrated Security=true;Database=BenchmarkViewer;";

        public BenchmarkData[] GetBenchmarks()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                return connection.Query<BenchmarkDb>("SELECT * FROM Benchmarks")
                    .Select(b => new BenchmarkData(b.Name, b.Id))
                    .ToArray();
            }
        }

        public void InsertResults(BenchmarkData benchmarkData)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {               
                connection.Open();
                                
                if (!BenchmarkExist(benchmarkData.BenchmarkName, connection))                
                    CreateBenchmark(benchmarkData.BenchmarkName, connection);

                var benchmarkID = GetBenchmarkID(benchmarkData.BenchmarkName, connection);

                InsertMeasurments(benchmarkData, connection, benchmarkID);                    
            }
        }

        public Measurement[] GetMeasurements(int benchmarkId, DateTime from, DateTime to)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                if (!BenchmarkExist(benchmarkId, connection))
                    return Array.Empty<Measurement>();

                var sql = "SELECT * FROM BenchmarkMeasurments WHERE BenchmarkID = @BenchmarkID " +
                    "AND Date BETWEEN @from AND @to";

                return connection.Query<Models.Database.MeasurementDb>(sql,
                    new { BenchmarkID = benchmarkId,
                        from,
                        to,
                        })
                    .Select(m => new Measurement(m.BenchmarkID, m.Date, m.Value, m.MetricName, m.Unit))
                    .ToArray();
            }
        }

        private bool BenchmarkExist(string benchmarkName, IDbConnection connection)
        {
            return connection.ExecuteScalar<bool>("SELECT COUNT (1) FROM Benchmarks WHERE Name = @BenchmarkName", new { BenchmarkName = benchmarkName });
        }

        private bool BenchmarkExist(int benchmarkId, IDbConnection connection)
        {
            return connection.ExecuteScalar<bool>("SELECT COUNT (1) FROM Benchmarks WHERE Id = @BenchmarkId", new { BenchmarkId = benchmarkId });
        }

        private void CreateBenchmark(string benchmarkName, IDbConnection connection)
        {
            connection.Execute("INSERT INTO Benchmarks VALUES (@Name)", new { Name = benchmarkName });
        }
        
        private int GetBenchmarkID(string benchmarkName, IDbConnection connection)
        {
            return connection.Query<int>("SELECT Id FROM Benchmarks WHERE Name = @BenchmarkName", new { BenchmarkName = benchmarkName }).Single();
        }

        private void InsertMeasurments(BenchmarkData benchmarkData, IDbConnection connection, int benchmarkID)
        {
            foreach (var item in benchmarkData.Measurements)
            {
                string sql = "INSERT INTO BenchmarkMeasurments (BenchmarkID, Date, Value, MetricName, Unit) VALUES (@BenchmarkID, @Date, @Value, @MetricName, @Unit)";

                connection.Execute(sql, 
                    new {
                        BenchmarkID = benchmarkID,
                        item.Date,
                        item.Value,
                        item.MetricName,
                        item.Unit
                });
            }
        }
    }
}