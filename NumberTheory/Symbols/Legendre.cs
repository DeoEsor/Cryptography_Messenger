namespace NumberTheory.Symbols
{
	public static class Legendre
	{
		public static BigInteger L(BigInteger a, BigInteger p)
		{
			if (a == 1) return 1;
			
			if (a % 2 == 0)
				return L(a / 2, p) * BigInteger.Pow(-1, (int)((BigInteger.Pow(p, 2) - 1) / 8));
			
			return (L(p % a, a) *
			                    (BigInteger)Math.Pow(-1,(double)((a - 1) * ((p - 1) / 4))));
		}
	}
}
