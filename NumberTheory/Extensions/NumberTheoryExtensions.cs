using NumberTheory.Euclid;
namespace NumberTheory.Extensions;

public static class NumberTheoryExtensions
{
	/// <summary>
	/// Finding all primes by a given module
	/// </summary>
	/// <param name="module">Module</param>
	/// <returns></returns>
	/// <remarks>https://e-maxx.ru/algo/reverse_element</remarks>
	/// <remarks>Complexity O(<paramref name="module"/>)</remarks>
	public static List<int> AllPrimesByModule(int module)
	{
		var r = new List<int>
		{
			1
		};
		for (var i=2; i<module; ++i)
			r.Add((module - (module/i) * r[module%i] % module) % module);
		return r;
	}

	/// <summary>
	/// Finds reverse 
	/// </summary>
	/// <param name="a">Value</param>
	/// <param name="module">Module</param>
	/// <returns>Reverse value</returns>
	/// <exception cref="ArithmeticException">No solution</exception>
	public static bool TryGetReverseByModule(BigInteger a, BigInteger module, out BigInteger result)
	{
		result = Int32.MinValue;
		var g = ExtendedGCD.Solve(a, module, out var x,out var y);

		if (g != 1)
			return false;
		result = (x % module + module) % module; 
		return true;
	}
}
