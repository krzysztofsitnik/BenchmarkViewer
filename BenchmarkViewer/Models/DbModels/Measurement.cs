using System;

namespace BenchmarkViewer.Models.DbModels
{    
    public class Measurement
    {
        public int BenchmarkID { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public string MetricName { get; set; }
        public string Unit { get; set; }

        public Measurement()
        {
        }
    }
}
