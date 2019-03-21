using System;
using Microsoft.AspNetCore.Mvc;
using BenchmarkViewer.Models.Contracts;
using BenchmarkViewer.Services;

namespace BenchmarkViewer.Controllers
{
    [Route("api/[controller]")]
    public class BenchmarksController : Controller
    {
        // GET: api /Benchmarks/5
        [HttpGet("{id}", Name = "Get")]
        public Measurement[] Get(string id)
        {
            var dataStorageService = new DataStorageService();

            return dataStorageService.GetMeasurements(id, DateTime.Now.AddMonths(-1), DateTime.Now);
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
    

