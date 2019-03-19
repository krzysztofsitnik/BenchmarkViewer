using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using BenchmarkViewer.Models.Contracts;
using BenchmarkViewer.Services;

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
            var dataStorageService = new DataStorageService();
            dataStorageService.InsertResults(value);
        }
    }
}
    

