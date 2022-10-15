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

    [Test]
    public void RightRotate()
    {
        var value = 1;
        Assert.Multiple(() =>
        {
            Assert.That(value.ShiftRotateRight(1), Is.EqualTo(int.MinValue));
            Assert.That(8.ShiftRotateRight(2), Is.EqualTo(2));
        });
    }
    
    [Test]
    public void LeftRotate()
    {
        var value = 1;
        Assert.Multiple(() =>
        {
            Assert.That(value.ShiftRotateLeft(5), Is.EqualTo(32));
            Assert.That(1.ShiftRotateLeft(32), Is.EqualTo(1));
        });
    }
}