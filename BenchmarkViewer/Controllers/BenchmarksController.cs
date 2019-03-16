using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using BenchmarkViewer.Models.Contracts;

namespace BenchmarkViewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenchmarksController : ControllerBase
    {
        const string ConnectionString = @"Server=.;Integrated Security=true;Database=BenchmarkViewer;";

        //GET: api/Benchmarks
        [HttpGet]
        public IEnumerable<int> Get()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var initialQuery = @"SELECT * FROM Benchmarks";

                var initialQueryResult = connection.Query<Models.Contracts.BenchmarkData>(initialQuery).ToArray();

                var measurmentsQuery = @"SELECT * FROM BenchmarkMeasurments";

                var measurmentsQueryResult = connection.Query<Models.Contracts.Measurement>(measurmentsQuery).ToArray();

                return initialQueryResult.Select(b => b.BenchmarkId);
            }
        }

        // GET: api /Benchmarks/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Benchmarks
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT: api/Benchmarks/
        [HttpPut()]
        public void Put([FromBody] Models.Contracts.BenchmarkData value)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string BenchmarkFullName = "test3";

                connection.Open();

                string query = "SELECT COUNT (1) FROM Benchmarks WHERE Benchmark = @Benchmark";

                bool DoesBenchmarkIDExist = false;

                DoesBenchmarkIDExist = connection.ExecuteScalar<bool>(query, new { Benchmark = BenchmarkFullName });

                if (DoesBenchmarkIDExist == true)
                {
                    var BenchmarkID = connection.Query<int>("SELECT BenchmarkID FROM Benchmarks WHERE Benchmark = @Benchmark", new { Benchmark = BenchmarkFullName }).Single();

                    string sql = "INSERT INTO BenchmarkMeasurments (BenchmarkID, Date, Value, MetricName, Unit) VALUES (@BenchmarkID, @Date, @Value, @MetricName, @Unit)";

                    var benchmarkMeasurment = new Measurement(BenchmarkID, DateTime.Now, 1, "Time", "Nanoseconds");

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
                else
                {

                    connection.Execute("INSERT INTO Benchmarks VALUES (@Benchmark)", new { Benchmark = BenchmarkFullName });

                    var BenchmarkID = connection.Query<int>("SELECT BenchmarkID FROM Benchmarks WHERE Benchmark = @Benchmark", new { Benchmark = BenchmarkFullName }).Single();

                    string sql = "INSERT INTO BenchmarkMeasurments (BenchmarkID, Date, Value, MetricName, Unit) VALUES (@BenchmarkID, @Date, @Value, @MetricName, @Unit)";

                    var benchmarkMeasurment = new Measurement(BenchmarkID, DateTime.Now, 1, "Time", "Nanoseconds");

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
    }
}
    

