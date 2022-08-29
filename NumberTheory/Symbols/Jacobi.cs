namespace NumberTheory.Symbols;

public static class Jacobi
{
	public static BigInteger J(BigInteger a, BigInteger b)
	{
		var r = 1;
		while (a != 0)
		{
			int t = 0;
			while ((a & 1) == 0)
			{
				t++;
				a >>= 1;
			}
			
			if ((t & 1) !=0)
			{
				var temp = b % 8;
				if (temp == 3 || temp == 5)
					r = -r;
			}
			
			BigInteger a4 = a % 4, b4 = b % 4;
			if (a4 == 3 && b4 == 3)
				r = -r;
			var c = a;
			a = b % c;
			b = c;
		}
		return r;
	}
}