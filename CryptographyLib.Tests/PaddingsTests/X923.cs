using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using CryptographyLib.Interfaces;
using PaddingMode = CryptographyLib.Paddings.Padding.PaddingMode;

namespace CryptographyLib.Tests.PaddingsTests;


[MinColumn, MaxColumn, MedianColumn]
[MarkdownExporterAttribute.GitHub]
[CsvExporter(CsvSeparator.Comma)]
[CsvMeasurementsExporter]
[TestFixture]
public class X923
{
    private IPadding Padding { get; set; } =  Paddings.Padding.CreateInstance(PaddingMode.X923);
    private byte[] Message { get; set; }
    
    [SetUp]
    public void SetUp()
    {
        Padding =  Paddings.Padding.CreateInstance(PaddingMode.X923);
    }
    
    [Test, Benchmark(Description = "PaddingTest")]
    public void PaddingTest()
    {
        Message = new byte[7];
        
        TestContext.CurrentContext.Random.NextBytes(Message);

        Message = Padding.ApplyPadding(Message, 16);
        Assert.That(Message.Length, Is.EqualTo(16));
    }
    
    [Test, Benchmark(Description = "DeletePaddingTest", Baseline = true)]
    public void DeletePaddingTest()
    {
        Message = new byte[7];
        
        TestContext.CurrentContext.Random.NextBytes(Message);

        Message = Padding.ApplyPadding(Message, 16);
        Message = Padding.DeletePadding(Message);
        Assert.That(Message.Length, Is.EqualTo(7));
    }
}