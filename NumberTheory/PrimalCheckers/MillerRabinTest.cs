using NumberTheory.Interfaces;

namespace NumberTheory.PrimalCheckers;

public class MillerRabinTest : IPrimalChecker
{
    private void GetComponents(BigInteger value, out BigInteger d, out BigInteger s)
    {
        s = 0;
        d = value - 1;
        while (d % 2 != 1)
        {
            d /= 2;
            s++;
        }
    }
    public bool Check(BigInteger value, float minProbability)
    {
        if (minProbability is not (> 0.49f and < 1))
            throw new ArgumentException($"{nameof(minProbability)} should be in range [0.75,1)",
                nameof(minProbability));
        
        if (value <= 0)
            throw new ArgumentException($"{nameof(value)} should be positive", nameof(value));
        
        var isPrime = true;
        var random = new Random();
        
        GetComponents(value, out var d, out var s);
        
        Parallel.For(0, IPrimalChecker.GetDegree(minProbability),
            (_, parallelState) =>
        {
            var randModPow = BigInteger.ModPow(random.Next(), d, value);
            
            if (randModPow == 1 || randModPow == value - 1) 
                return;
            
            for (var i = 1; i < s; i++)
                if ((randModPow = BigInteger.ModPow(randModPow,2,value)) == value - 1) 
                    return;

            isPrime = false;
            parallelState.Break();
        });

        return isPrime;
    }
}