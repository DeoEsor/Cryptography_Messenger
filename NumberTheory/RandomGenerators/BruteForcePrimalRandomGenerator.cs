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
        var value = new BigInteger(min.ToByteArray()) * 10;

        while (!PrimalChecker.Check(value, 0.95f)) 
            value ++;

        return value;
    }
}