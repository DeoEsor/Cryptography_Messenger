using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;

namespace CryptographyLib.Tests.PaddingsTests;

[TestFixture]
public class Benchmark
{
    [Test]
    public void TestMethod1()
    {
        var config = new ManualConfig()
            .WithOptions(ConfigOptions.DisableOptimizationsValidator)
            .AddLogger(ConsoleLogger.Ascii);

        
        BenchmarkRunner.Run<X923>(config);
    }
}