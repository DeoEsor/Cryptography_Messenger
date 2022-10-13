namespace CryptographyLib.Extensions;

public static class FuncExtensions
{
	/// <summary>
	/// Creating a key with rule
	/// </summary>
	/// <param name="a">Rule of creating key</param>
	/// <param name="k">Count of bytes in key</param>
	/// <param name="expectedValues">values that could be</param>
	/// <remarks><paramref name="expectedValues"/> - optional param</remarks>
	/// <returns>associative array (key)</returns>
	internal static Dictionary<byte, byte> CreateKeyByFunc(this Func<byte, byte> a, int k,
		params byte[] expectedValues)
	{
		var result = new Dictionary<byte, byte>();
		if (expectedValues.Length == 0)
			for (byte i = 0; i < k; i++)
				result.Add(i, a(i));
			
		else
			foreach (var expectedValue in expectedValues)
				result.Add(expectedValue, a(expectedValue));

			
		return result;
	}
}