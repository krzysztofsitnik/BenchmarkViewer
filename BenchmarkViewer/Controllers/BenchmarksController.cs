using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;

namespace BenchmarkViewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenchmarksController : ControllerBase
    {
        const string ConnectionString = @"Server=.;Integrated Security=true;Database=BenchmarkViewer;";

        // GET: api/Benchmarks
        [HttpGet]
        public IEnumerable<string> Get()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var query = @"SELECT * FROM Benchmarks";

                var result = connection.Query<Models.DbModels.Benchmark>(query).ToArray();

                return result.Select(b => b.BenchmarkId);
            }
        }

        // GET: api/Benchmarks/5
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

        }
    }
}
