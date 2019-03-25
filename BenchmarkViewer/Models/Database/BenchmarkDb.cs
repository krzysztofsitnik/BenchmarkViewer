namespace BenchmarkViewer.Models.Database
{
    public class BenchmarkDb
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public BenchmarkDb() { } // public parameterless ctor is required by Dapper
    }
}
