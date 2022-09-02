using NumberTheory.Extensions.Arithmetic;
using NumberTheory.Interfaces;
using NUnit.Framework;

namespace NumberTheory.PrimalCheckers;

public class FermatTest : IPrimalChecker
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="minProbability"></param>
    /// <exception cref="ArgumentException"></exception>
    /// <returns>Is <param name="value"/> can be called pseudo-primal </returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool Check(BigInteger value, float minProbability)
    {
        if (minProbability is not (> 0.49f and < 1))
            throw new ArgumentException($"{nameof(minProbability)} should be in range [0.5,1)",
                nameof(minProbability));
        
        if (value <= 0)
            throw new ArgumentException($"{nameof(value)} should be positive", nameof(value));
        
        if (value <= 3)
            return true;
        
        var isPrime = true;
        
        Parallel.For(0, IPrimalChecker.GetDegree(minProbability), 
            (_, parallelState) =>
        {
            var a = TestContext.CurrentContext.Random.NextULong() % (value - 2) + 2;

            if (BigInteger.GreatestCommonDivisor(a, value) == 1 &&
                ArithmeticExtensions.PowMod(a, value - 1, value) == 1) return;
            isPrime = false;
            parallelState.Break();
        });

        return isPrime;
    }
}