using System;

namespace BenchmarkViewer.Models.Contracts
{
    public class BenchmarkData
    {
        public string BenchmarkName { get; set; }
        public int BenchmarkId { get; set; }
        public Measurement[] Measurements { get; set; }

        public BenchmarkData(string Benchmark)
        {
            this.BenchmarkName = Benchmark;
        }

        public BenchmarkData(string benchmark, int benchmarkId)
        {
            this.BenchmarkName = benchmark;
            this.BenchmarkId = benchmarkId;
        }

        public BenchmarkData(string benchmark, int benchmarkId, Measurement[] measurements)
        {
            this.BenchmarkName = benchmark;
            this.BenchmarkId = benchmarkId;
            this.Measurements = measurements;
        }
    }

    public class Measurement
    {
        public int BenchmarkID { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public string MetricName { get; set; }
        public string Unit { get; set; }

        public Measurement (int BenchmarkID, DateTime Date, Double Value, string MetricName, string Unit)
        {
            this.BenchmarkID = BenchmarkID;
            this.Date = Date;
            this.Value = Value;
            this.MetricName = MetricName;
            this.Unit = Unit;
        }
    }
}
    