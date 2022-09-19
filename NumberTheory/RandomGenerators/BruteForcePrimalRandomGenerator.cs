using NumberTheory.Interfaces;
using NUnit.Framework;
namespace NumberTheory.RandomGenerators;

public class BruteForcePrimalRandomGenerator : PrimalRandomGenerator
{
    public BruteForcePrimalRandomGenerator(IPrimalChecker primalChecker) 
        : base(primalChecker)
    {}

    public override BigInteger Generate(BigInteger min, BigInteger max)
    {
        var buffer = max.ToByteArray();
        var value = min + 1;

        while (!PrimalChecker.Check(value, 0.95f) && value < max)
        {
            TestContext.CurrentContext.Random.NextBytes(buffer);
            value = new BigInteger(buffer);
        }

        return value;
    }
}