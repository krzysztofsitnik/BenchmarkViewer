using DataTransferContracts;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using BenchmarkViewer.Models.Contracts;
using System.Collections.Generic;

namespace BenchmarkViewer.Services
{
    public class DataGenerator
    {
        private const string FullBdnJsonFileExtension = "full.json";

        private static readonly DateTime[] Dates = Enumerable.Repeat(DateTime.Now, 7).Select((date, index) => date.AddDays(index * -1)).ToArray();

        public static void Generate(string resultsDirectoryPath)
        {
            var dataStorageService = new DataStorageService();
            var random = new Random(1245);

            var resultFiles = Directory.GetFiles(resultsDirectoryPath, $"*{FullBdnJsonFileExtension}", SearchOption.AllDirectories);

            foreach (var resultFile in resultFiles)
            {
                var bdnResult = ReadFromFile(resultFile);

                foreach (var benchmark in bdnResult.Benchmarks.Where(benchmark => benchmark.FullName.Length < 512))
                {
                    foreach (var date in Dates)
                    {
                        var benchmarkData = new BenchmarkData(benchmark.FullName);

                        benchmarkData.Measurements = benchmark.GetOriginalValues().Select((m, index) =>
                            new Models.Contracts.Measurement(
                                0, // set by the service
                                date.AddHours(index),
                                m * random.Next(95, 105) / 100.0,
                                "Time",
                                "ns"))
                            .ToArray();

                        dataStorageService.InsertResults(benchmarkData);
                    }
                }
            }
        }

        private static BdnResult ReadFromFile(string resultFilePath)
        {
            try
            {
                return JsonConvert.DeserializeObject<BdnResult>(File.ReadAllText(resultFilePath));
            }
            catch (JsonSerializationException)
            {
                Console.WriteLine($"Exception while reading the {resultFilePath} file.");

                throw;
            }
        }
    }
}

namespace DataTransferContracts // generated with http://json2csharp.com/#
{
    public class ChronometerFrequency
    {
        public int Hertz { get; set; }
    }

    public class HostEnvironmentInfo
    {
        public string BenchmarkDotNetCaption { get; set; }
        public string BenchmarkDotNetVersion { get; set; }
        public string OsVersion { get; set; }
        public string ProcessorName { get; set; }
        public int? PhysicalProcessorCount { get; set; }
        public int? PhysicalCoreCount { get; set; }
        public int? LogicalCoreCount { get; set; }
        public string RuntimeVersion { get; set; }
        public string Architecture { get; set; }
        public bool? HasAttachedDebugger { get; set; }
        public bool? HasRyuJit { get; set; }
        public string Configuration { get; set; }
        public string JitModules { get; set; }
        public string DotNetCliVersion { get; set; }
        public ChronometerFrequency ChronometerFrequency { get; set; }
        public string HardwareTimerKind { get; set; }
    }

    public class ConfidenceInterval
    {
        public int N { get; set; }
        public double Mean { get; set; }
        public double StandardError { get; set; }
        public int Level { get; set; }
        public double Margin { get; set; }
        public double Lower { get; set; }
        public double Upper { get; set; }
    }

    public class Percentiles
    {
        public double P0 { get; set; }
        public double P25 { get; set; }
        public double P50 { get; set; }
        public double P67 { get; set; }
        public double P80 { get; set; }
        public double P85 { get; set; }
        public double P90 { get; set; }
        public double P95 { get; set; }
        public double P100 { get; set; }
    }

    public class Statistics
    {
        public int N { get; set; }
        public double Min { get; set; }
        public double LowerFence { get; set; }
        public double Q1 { get; set; }
        public double Median { get; set; }
        public double Mean { get; set; }
        public double Q3 { get; set; }
        public double UpperFence { get; set; }
        public double Max { get; set; }
        public double InterquartileRange { get; set; }
        public List<double> LowerOutliers { get; set; }
        public List<double> UpperOutliers { get; set; }
        public List<double> AllOutliers { get; set; }
        public double StandardError { get; set; }
        public double Variance { get; set; }
        public double StandardDeviation { get; set; }
        public double Skewness { get; set; }
        public double Kurtosis { get; set; }
        public ConfidenceInterval ConfidenceInterval { get; set; }
        public Percentiles Percentiles { get; set; }
    }

    public class Memory
    {
        public int Gen0Collections { get; set; }
        public int Gen1Collections { get; set; }
        public int Gen2Collections { get; set; }
        public int TotalOperations { get; set; }
        public long BytesAllocatedPerOperation { get; set; }
    }

    public class Measurement
    {
        public string IterationStage { get; set; }
        public int LaunchIndex { get; set; }
        public int IterationIndex { get; set; }
        public long Operations { get; set; }
        public double Nanoseconds { get; set; }
    }

    public class Benchmark
    {
        public string DisplayInfo { get; set; }
        public object Namespace { get; set; }
        public string Type { get; set; }
        public string Method { get; set; }
        public string MethodTitle { get; set; }
        public string Parameters { get; set; }
        public string FullName { get; set; }
        public Statistics Statistics { get; set; }
        public Memory Memory { get; set; }
        public List<Measurement> Measurements { get; set; }

        /// <summary>
        /// this method was not auto-generated by a tool, it was added manually
        /// </summary>
        /// <returns>an array of the actual workload results (not warmup, not pilot)</returns>
        internal double[] GetOriginalValues()
            => Measurements
                .Where(measurement => measurement.IterationStage == "Result")
                .Select(measurement => measurement.Nanoseconds / measurement.Operations)
                .ToArray();
    }

    public class BdnResult
    {
        public string Title { get; set; }
        public HostEnvironmentInfo HostEnvironmentInfo { get; set; }
        public List<Benchmark> Benchmarks { get; set; }
    }
}
