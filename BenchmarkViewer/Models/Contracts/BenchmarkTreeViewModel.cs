using System.Collections.Generic;

namespace BenchmarkViewer.Models.Contracts
{
    public class BenchmarkTreeViewModel
    {
        public string Text { get; set; }
        public int Id { get; set; }
        public List<BenchmarkTreeViewModel> Children { get; set; } = new List<BenchmarkTreeViewModel>();
    }
}
