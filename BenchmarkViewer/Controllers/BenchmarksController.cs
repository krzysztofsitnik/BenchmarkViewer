using System;
using Microsoft.AspNetCore.Mvc;
using BenchmarkViewer.Models.Contracts;
using BenchmarkViewer.Services;

namespace BenchmarkViewer.Controllers
{
    [Route("api/[controller]")]
    public class BenchmarksController : Controller
    {
        [HttpGet("[action]")]
        public Measurement[] Get()
        {
            string BenchmarkName = "test";

            DateTime from = new DateTime(2019, 1, 1);

            DateTime to = new DateTime(2019, 12, 1);

            var dataStorageService = new DataStorageService();

            return dataStorageService.GetMeasurements(BenchmarkName, from, to);
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
    

