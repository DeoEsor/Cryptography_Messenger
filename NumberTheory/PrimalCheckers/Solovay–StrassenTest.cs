using NumberTheory.Extensions.Arithmetic;
using NumberTheory.Interfaces;
using NumberTheory.Symbols;

namespace NumberTheory.PrimalCheckers;

public class Solovay_StrassenTest : IPrimalChecker
{
    public bool Check(BigInteger value, float minProbability)
    {
        if (value < 2)
            return false;
        if (value != 2 && value % 2 == 0)
            return false;
 
        var random = new Random();
        bool isPrime = true;
        Parallel.For(0, IPrimalChecker.GetDegree(minProbability), (i, parallelState) =>
        {
            var a = random.NextInt64() % (value - 1) + 1;
            var jacobian = (value + Jacobi.J(a, value)) % value;
            var mod = ArithmeticExtensions.PowMod(a, (value - 1) / 2, value);

            if (jacobian == 0 || mod != jacobian)
            {
                isPrime = false;
                parallelState.Break();
            }
        });
        return isPrime;
    }
}