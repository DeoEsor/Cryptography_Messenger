namespace NumberTheory.Extensions.Arithmetic;
public static partial class ArithmeticExtensions
{ 
	static string? _a = string.Empty;
	/// <summary>
	/// Binary exponentiation
	/// </summary>
	/// <param name="a">Base</param>
	/// <param name="n">Power</param>
	/// <returns>a ^ n</returns>
	/// <remarks>Complexity O(log( <paramref name="n"/> ))</remarks>
	public static BigInteger Pow(BigInteger a, BigInteger n) 
	{
		BigInteger res = 1;
		while (n != 0)
		{
			if ((n & 0b1) == 1)
				res *= a;
			a *= a;
			n >>= 1;
		}
		return res;
	}
	
	public static int Pow(int a, int n) 
	{
		int res = 1;
		while (n != 0)
		{
			if ((n & 0b1) == 1)
				res *= a;
			a *= a;
			n >>= 1;
		}
		return res;
	}
	
	
	public static BigInteger MultiplyModule(BigInteger a, BigInteger b, BigInteger mod) // умножение чисел, чтобы не было переполнения 
	{
		if (b == 1)
			return a;

		if (b % 2 == 0)
		{
			BigInteger t = MultiplyModule(a, b / 2, mod);
			return (2 * t)%mod;
		}
		return (MultiplyModule(a, b - 1, mod) + a) % mod;
	}
	
	public static BigInteger PowMod(BigInteger a, BigInteger n, BigInteger mod) 
	{
		BigInteger p = 1;
		while (n > 0)
		{
			if (n % 2 != 0)
			{
				p = MultiplyModule(p, a, mod);
				n--;
			}
			n /= 2;
			a = MultiplyModule(a, a, mod);
		}
		return p % mod;
	}
}

