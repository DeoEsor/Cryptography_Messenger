using NumberTheory.Symbols;

namespace NumberTheory.Extensions;

public static class BigIntegerExtensions
{
    public static BigInteger[] DeserializeBigInts(this byte[] bytes)
    {
        var res = new BigInteger[BitConverter.ToInt32(bytes.AsSpan(0, 4))];
        var startIndex = 4;

        for (var i = 0; i < res.Length; i++)
        {
            var length = BitConverter.ToInt32(bytes.AsSpan(startIndex, 4));
            var value = new BigInteger(bytes.AsSpan(startIndex + 4, length));
            startIndex += 4 + length;
            res[i] = value;
        }
		
        return res;
    }

    public static byte[] SerializeBigInts(this BigInteger[] array)
    {
        var res = new List<byte>(); 
        res.AddRange(BitConverter.GetBytes(array.Length));

        foreach (var value in array)
        {
            var bytes = value.ToByteArray();
            res.AddRange(BitConverter.GetBytes(bytes.Length));
            res.AddRange(bytes);
        }

        return res.ToArray();
    }
    
    public static BigInteger Sqrt(this BigInteger n)
    {
        if (n == 0) return 0;
        
        if (n < 0) throw new ArithmeticException("NaN");
        
        var bitLength = Convert
            .ToInt32(Math.Ceiling(BigInteger.Log(n, 2)));
        
        var root = BigInteger.One << (bitLength / 2);

        while (!IsSqrt(n, root))
        {
            root += n / root;
            root /= 2;
        }

        return root;

    }

    private static bool IsSqrt(BigInteger n, BigInteger root)
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

    public static BigInteger GetPrimalSqrt(this BigInteger p)
    {
        var fact = new List<BigInteger>();
        BigInteger phi = Euler.EulerFunc(p),  n = phi;
        for (var i=2; i*i<=n; ++i)
            if (n % i == 0)
            {
                fact.Add(i);
                while (n % i == 0)
                    n /= i;
            }
        
        if (n > 1)
            fact.Add (n);
 
        for (var res=2; res<=p; ++res) 
        {
            var ok = true;
            
            for (var i=0; i< fact.Count && ok; ++i)
                ok &= BigInteger.ModPow (res, phi / fact[i], p) != 1;
            
            if (ok)  
                return res;
        }
        return -1;
    }

    public static BigInteger Pow(this BigInteger value, BigInteger degree)
    {
        for (BigInteger i = 0; i < degree; i++)
            value = BigInteger.Multiply(value, value);
        return value;
    }
    
    
    public static BigInteger MultMod(this BigInteger a, BigInteger b, BigInteger mod)
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

    public static BigInteger FastPow(this BigInteger num, BigInteger pow, BigInteger mod) // a^b mod n - то же что ниже но быстрее
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