using System;

namespace BenchmarkViewer.Models.Contracts
{
    public class BenchmarkData
    {
        public string Benchmark { get; set; }
        public int BenchmarkId { get; set; }

        public Measurement[] Measurements { get; set; }
    }

    public class Measurement
    {
        public int BenchmararkID { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public string MetricName { get; set; }
        public string Unit { get; set; }
    }
}
