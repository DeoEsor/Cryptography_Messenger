using CryptographyLib.Interfaces;
using CryptographyLib.Paddings;
using PaddingMode = CryptographyLib.Paddings.Padding.PaddingMode;

namespace CryptographyLib.Tests.PaddingsTests;

[TestFixture]
public class X923
{
    private IPadding Padding { get; set; }
    private byte[] Message { get; set; }
    
    [SetUp]
    public void SetUp()
    {
        Padding =  Paddings.Padding.CreateInstance(PaddingMode.X923);
    }
    
    [Test]
    public void PaddingTest()
    {
        Message = new byte[7];
        
        TestContext.CurrentContext.Random.NextBytes(Message);

        Message = Padding.ApplyPadding(Message, 16);
        Assert.That(Message.Length, Is.EqualTo(16));
    }
    
    [Test]
    public void DeletePaddingTest()
    {
        Message = new byte[7];
        
        TestContext.CurrentContext.Random.NextBytes(Message);

        Message = Padding.ApplyPadding(Message, 16);
        Message = Padding.DeletePadding(Message);
        Assert.That(Message.Length, Is.EqualTo(7));
    }
}