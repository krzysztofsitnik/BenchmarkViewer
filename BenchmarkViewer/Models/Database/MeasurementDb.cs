using System;

namespace BenchmarkViewer.Models.Database
{    
    public class MeasurementDb
    {
        public int BenchmarkID { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public string MetricName { get; set; }
        public string Unit { get; set; }

        public MeasurementDb() { } // public parameterless ctor is required by Dapper
    }
}
