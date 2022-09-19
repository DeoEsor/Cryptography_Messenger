namespace NumberTheory.Tests;

[TestFixture]
public class ProbabilityTests
{
    [Test]
    public void FermatTest()
    {
        var fermat = new FermatTest();

        Assert.That(fermat.Check(1000003,0.87f), Is.EqualTo(true));
    }
    
    [Test]
    public void Solovay_StrassenTest()
    {
        var solovay = new SolovayStrassenTest();

        Assert.That(solovay.Check(1000003,0.87f), Is.EqualTo(true));
    }
    
    [Test]
    public void MillerRabinTest()
    {
        var millerRabin = new MillerRabinTest();

        Assert.That(millerRabin.Check(1000003,0.87f), Is.EqualTo(true));
    }
}