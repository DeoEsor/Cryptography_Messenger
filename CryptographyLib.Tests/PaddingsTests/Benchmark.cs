using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Perfolizer.Horology;
using Perfolizer.Mathematics.SignificanceTesting;
using Perfolizer.Mathematics.Thresholds;

namespace CryptographyLib.Tests.PaddingsTests;

[TestFixture]
public class Benchmark
{
    [Test]
    public void TestMethod1()
    {
        var config = new ManualConfig()
            .WithOptions(ConfigOptions.DisableOptimizationsValidator)
            .AddLogger(ConsoleLogger.Ascii)
            .AddExporter(new CsvExporter(CsvSeparator.Comma))
            .WithSummaryStyle(SummaryStyle.Default)
            .AddJob(Job.ShortRun);

        
        BenchmarkRunner.Run<X923>(config);
        BenchmarkRunner.Run<Pkcs7>(config);
    }
}