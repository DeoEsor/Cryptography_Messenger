using NumberTheory.Interfaces;

namespace NumberTheory.RandomGenerators;

public abstract class PrimalRandomGenerator : IRandomGenerator
{
    public IPrimalChecker PrimalChecker { get; set; }
    
    protected PrimalRandomGenerator(IPrimalChecker primalChecker)
    {
        PrimalChecker = primalChecker;
    }

    public abstract BigInteger Generate(BigInteger min, BigInteger max);


    public BigInteger Generate() => Generate(BigInteger.MinusOne, BigInteger.MinusOne);
}