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
        // GET: api /Benchmarks/id/from/to
        [HttpGet("{id}/{from}/{to}", Name = "Get")]
        public BenchmarkData Get(int id, DateTime from, DateTime to)
        {
            var dataStorageService = new DataStorageService();

            return dataStorageService.GetBenchmarkData(id, from, to);
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