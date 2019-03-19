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
                                
                if (!BenchmarkExist(benchmarkData, connection))                
                    CreateBenchmark(benchmarkData, connection);

                var benchmarkID = GetBenchmarkID(benchmarkData, connection);

                InsertMeasurments(benchmarkData, connection, benchmarkID);                    
            }
        }

        private bool BenchmarkExist(BenchmarkData benchmarkData, IDbConnection connection)
        {
            return connection.ExecuteScalar<bool>("SELECT COUNT (1) FROM Benchmarks WHERE Benchmark = @Benchmark", new { Benchmark = benchmarkData.BenchmarkName });
        }

        private void CreateBenchmark(BenchmarkData benchmarkData, IDbConnection connection)
        {
            connection.Execute("INSERT INTO Benchmarks VALUES (@Benchmark)", new { Benchmark = benchmarkData.BenchmarkName });
        }
        
        private int GetBenchmarkID(BenchmarkData benchmarkData, IDbConnection connection)
        {
            return connection.Query<int>("SELECT BenchmarkID FROM Benchmarks WHERE Benchmark = @Benchmark", new { Benchmark = benchmarkData.BenchmarkName }).Single();
        }
        //TODO update to insert whole array instead of hardcoded
        private void InsertMeasurments(BenchmarkData benchmarkData, IDbConnection connection, int benchmarkID)
        {
            string sql = "INSERT INTO BenchmarkMeasurments (BenchmarkID, Date, Value, MetricName, Unit) VALUES (@BenchmarkID, @Date, @Value, @MetricName, @Unit)";

            var benchmarkMeasurment = new Measurement(benchmarkID, DateTime.Now, 1, "Time", "Nanoseconds");

            var insertDetails = connection.Execute(sql,
                new
                {
                    BenchmarkID = benchmarkMeasurment.BenchmarkID,
                    Date = benchmarkMeasurment.Date,
                    Value = benchmarkMeasurment.Value,
                    MetricName = benchmarkMeasurment.MetricName,
                    Unit = benchmarkMeasurment.Unit
                });
        }
    }
}
