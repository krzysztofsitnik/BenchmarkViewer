using System;

namespace BenchmarkViewer.Models.Contracts
{
    public class BenchmarkData
    {
        public string BenchmarkId { get; set; }

        public Measurement[] Measurements { get; set; }
    }

    public class Measurement
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public string MetricName { get; set; }
        public DateTime When { get; set; }
    }
}
