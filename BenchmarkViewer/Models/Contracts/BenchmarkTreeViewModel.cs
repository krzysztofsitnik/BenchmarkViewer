using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenchmarkViewer.Models.Contracts
{
    public class BenchmarkTreeViewModel
    {
        public string Text { get; set; }
        public int Id { get; set; }
        public BenchmarkTreeViewModel[] Children { get; set; }
    }
}
