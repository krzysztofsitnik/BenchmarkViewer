using System;
using Microsoft.AspNetCore.Mvc;
using BenchmarkViewer.Models.Contracts;
using BenchmarkViewer.Services;
using System.Collections.Generic;

namespace BenchmarkViewer.Controllers
{
    [Route("api/[controller]")]
    public class BenchmarksController : Controller
    {
        // GET: api /Benchmarks/5
        [HttpGet("{id}", Name = "Get")]
        public Measurement[] Get(int id)
        {
            var dataStorageService = new DataStorageService();

            return dataStorageService.GetMeasurements(id, DateTime.Now.AddMonths(-1), DateTime.Now);
        }

        // GET: api/Benchmarks
        [HttpGet]
        public IReadOnlyList<BenchmarkTreeViewModel> GetTreeView()
        {
            return TreeViewService.BuildTreeView();
        }

        // PUT: api/Benchmarks/
        [HttpPut()]
        public void Put([FromBody] BenchmarkData value)
        {
            var dataStorageService = new DataStorageService();
            dataStorageService.InsertResults(value);
        }
    }
}
    

