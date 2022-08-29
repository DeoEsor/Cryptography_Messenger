namespace NumberTheory.Extensions;

public static class BigIntegerExtensions
{
    public static BigInteger Sqrt(this BigInteger n)
    {
        if (n == 0) return 0;
        if (n > 0)
        {
            int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(n, 2)));
            var root = BigInteger.One << (bitLength / 2);

            while (!isSqrt(n, root))
            {
                root += n / root;
                root /= 2;
            }

            return root;
        }

        throw new ArithmeticException("NaN");
    }

    private static bool isSqrt(BigInteger n, BigInteger root)
    {
        var lowerBound = root*root;
        var upperBound = (root + 1)*(root + 1);

        return (n >= lowerBound && n < upperBound);
    }
    
    public static IEnumerable<(BigInteger,BigInteger)> GetConvergent(this IEnumerable<BigInteger> cf) 
    {
        BigInteger   r = 0, 
            s = 1,
            p = 1, 
            q = 0;
        foreach (var c in cf) 
        {
            var tempP = p;
            var tempQ = q;
            p = c * p + r;
            q = c * q + s;
            r = tempP;
            s = tempQ;

            yield return (p,q);
        }
    }
    
    public static IEnumerable<BigInteger> CFRAC(BigInteger p, BigInteger q) 
    {
        while (q != 0) 
        {
            var n = p / q;
            yield return n;
            var extra = p - q * n;
            p = q;
            q = extra;
        }
    }
    
    public static BigInteger MultMod(BigInteger a, BigInteger b, BigInteger mod)
    {
        BigInteger res = 0; // Initialize result
        a %= mod;
		
        while (b > 0)
        {
            if ((b & 1) > 0)
                res = (res + a) % mod;
            a = (2 * a) % mod;
            b >>= 1; // b = b / 2
        }
        return res;
    }

    public static BigInteger FastPow(BigInteger num, BigInteger pow, BigInteger mod) // a^b mod n - то же что ниже но быстрее
    {
        BigInteger res = 1;
        while (pow > 0)
        {
            if (pow % 2 == 1) res = (res * num) % mod;
            num = (num * num) % mod;
            pow >>= 1;
        }

        return res;
    }
}