using NumberTheory.Extensions.Arithmetic;
using NumberTheory.Interfaces;
using NumberTheory.PrimalCheckers;
using NUnit.Framework;

namespace CryptoTest.NumberTheory;

[TestFixture]
public partial class NumberTheory
{
    [Test]
    public void BinaryDivide()
    {
        Assert.AreEqual((0, 1),
            ArithmeticExtensions.unsigned_divide(1, 2));
    }
    
    [Test]
    public void BinaryPow()
    {
        Assert.AreEqual(25,
            ArithmeticExtensions.Pow(5, 2));
    }
    
    [Test]
    public void Xor()
    {
        Assert.AreEqual(7,
            ArithmeticExtensions.XOR(5, 2));
    }

    [Test]
    public void GetDegree()
    {
        Assert.AreEqual(3, IPrimalChecker.GetDegree(0.8f));
    }
    
    [Test]
    public void FermatTest()
    {
        var fermat = new FermatTest();

        Assert.AreEqual(true, fermat.Check(1000003,0.87f));
    }
    
    [Test]
    public void Solovay_StrassenTest()
    {
        var solovay = new Solovay_StrassenTest();

        Assert.AreEqual(true, solovay.Check(1000003,0.87f));
    }
    
    [Test]
    public void MillerRabinTest()
    {
        var millerRabin = new MillerRabinTest();

        Assert.AreEqual(true, millerRabin.Check(1000003,0.87f));
    }
}