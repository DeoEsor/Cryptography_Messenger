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
    /// <returns>Is <param name="value"/> can be called pseudo-primal </returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool Check(BigInteger value, float minProbability)
    {
        if (minProbability is not (> 0.49f and < 1))
            throw new ArgumentException(nameof(minProbability));
        if (value <= 0)
            throw new ArgumentException(nameof(value));
        
        if (value <= 3)
            return true;
        var random = new Random();
        var isPrime = true;

        Parallel.For(0, IPrimalChecker.GetDegree(minProbability), (i, parallelState) =>
        { //TODO Concurrent collection to add generated random numbers
            var a = TestContext.CurrentContext.Random.NextULong() % (value - 2) + 2; 
		
            if (BigInteger.GreatestCommonDivisor(a, value) != 1 // если числа не взаимно просты, то понятно, что p не простое
                || ArithmeticExtensions.PowMod(a, value-1,value) != 1)  // a^(p-1) = 1 (mod p)
            {
                isPrime = false;
                parallelState.Break();
            }
        });

        return isPrime;
    }
}