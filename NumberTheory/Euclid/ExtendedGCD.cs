namespace NumberTheory.Euclid;

public static class ExtendedGCD
{
	public static BigInteger Solve(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
	{
		if (a == 0) 
		{
			x = 0;
			y = 1;
			return b;
		}
		var d = Solve(b % a, a, out var x1, out var  y1);
		x = y1 - (b / a) * x1;
		y = x1;
		return d;
	}
}