using BenchmarkViewer.Models.Contracts;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BenchmarkViewer.Services
{
    public class DataStorageService
    {
        const string ConnectionString = @"Server=.;Integrated Security=true;Database=BenchmarkViewer;";

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

        public Measurement[] GetMeasurements(string benchmarkName, DateTime from, DateTime to)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                if (!BenchmarkExist(benchmarkName, connection))
                    return Array.Empty<Measurement>();

                var benchmarkID = GetBenchmarkID(benchmarkName, connection);

                var sql = "SELECT Date, Value, MetricName, Unit FROM BenchmarkMeasurments WHERE BenchmarkID = @BenchmarkID " +
                    "AND Date BETWEEN @from AND @to";

                return connection.Query<Models.DbModels.Measurement>(sql,
                    new {
                        BenchmarkID = benchmarkID,
                        from,
                        to,
                    })
                    .Select(m => new Measurement(m.BenchmarkID, m.Date, m.Value, m.MetricName, m.Unit))
                    .ToArray();
            }
        }

        private bool BenchmarkExist(string benchmarkName, IDbConnection connection)
        {
            return connection.ExecuteScalar<bool>("SELECT COUNT (1) FROM Benchmarks WHERE Benchmark = @Benchmark", new { Benchmark = benchmarkName });
        }

        private void CreateBenchmark(string benchmarkName, IDbConnection connection)
        {
            connection.Execute("INSERT INTO Benchmarks VALUES (@Benchmark)", new { Benchmark = benchmarkName });
        }
        
        private int GetBenchmarkID(string benchmarkName, IDbConnection connection)
        {
            return connection.Query<int>("SELECT BenchmarkID FROM Benchmarks WHERE Benchmark = @Benchmark", new { Benchmark = benchmarkName }).Single();
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
