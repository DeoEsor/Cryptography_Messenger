namespace NumberTheory.Symbols;

public static class Euler
{
	/// <inheritdoc cref="Phi"/>
	public static BigInteger EulerFunc(BigInteger n) => Phi(n);
		
	/// <summary>
	/// Euler function
	/// </summary>
	/// <param name="n">Number</param>
	/// <returns>
	/// Count from 1 to <paramref name="n"/> coprime/
	/// relatively prime or mutually prime
	/// </returns>
	/// <remarks>Complexity O(sqrt(<paramref name="n"/>))</remarks>
	private static BigInteger Phi(BigInteger n) 
	{
		BigInteger result = n;
			
		for (BigInteger i=2; i*i<=n; ++i)
			if (n % i == 0) 
			{
				while (n % i == 0)
					n /= i;
				result -= result / i;
			}
			
		if (n > 1)
			result -= result / n;
		return result;
	}
}