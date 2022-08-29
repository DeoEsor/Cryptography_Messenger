using NumberTheory.Extensions.Arithmetic;
using NumberTheory.Interfaces;
using NumberTheory.Symbols;

namespace NumberTheory.PrimalCheckers;

public class SolovayStrassenTest : IPrimalChecker
{
    public bool Check(BigInteger value, float minProbability)
    {
        if (value < 2
            || (value != 2 && value % 2 == 0))
            return false;
 
        var random = new Random();
        var isPrime = true;
        Parallel.For(0, IPrimalChecker.GetDegree(minProbability), 
            (_, parallelState) =>
        {
            var a = random.NextInt64() % (value - 1) + 1;
            var j = Jacobi.J(a, value) % value;
            var mod = ArithmeticExtensions.PowMod(a, (value - 1) / 2, value);

            if (j != 0 && mod == j) return;
            
            isPrime = false;
            parallelState.Break();
        });
        
        return isPrime;
    }
}