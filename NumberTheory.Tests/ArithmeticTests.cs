namespace NumberTheory.Tests;

[TestFixture]
public class ArithmeticTests
{
    [Test]
    public void BinaryDivide()
    {
        Assert.That(ArithmeticExtensions.unsigned_divide(1, 2), Is.EqualTo((0, 1)));
    }
    
    [Test]
    public void BinaryPow()
    {
        Assert.That(ArithmeticExtensions.Pow(5, 2), Is.EqualTo(25));
    }
    
    [Test]
    public void Xor()
    {
        Assert.That(ArithmeticExtensions.XOR(5, 2), Is.EqualTo(7));
    }

    [Test]
    public void GetDegree()
    {
        Assert.That(IPrimalChecker.GetDegree(0.8f), Is.EqualTo(3));
    }
}