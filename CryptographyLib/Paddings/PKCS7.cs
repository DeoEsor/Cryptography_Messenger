using NumberTheory.Extensions.Arithmetic;
// ReSharper disable InconsistentNaming
namespace CryptographyLib.Paddings;

/// <summary>
/// https://datatracker.ietf.org/doc/html/rfc3369
/// </summary>
internal class PKCS7 : IPadding
{
	/// <summary>
	/// Padding byte array in format of PKCS7
	/// https://ru.wikipedia.org/wiki/Дополнение_(криптография)#PKCS7
	/// </summary>
	/// <param name="input"></param>
	/// <param name="blockLength"></param>
	/// <returns></returns>
	public byte[] ApplyPadding(byte[] input, int blockLength)
	{
		var reqPadding= ArithmeticExtensions.
			unsigned_divide((uint)input.Length, (byte)blockLength)
			.Item2;
		if (reqPadding == 0)
			return input;
		var res = new byte[input.Length + reqPadding];

		for (var i = 0; i < input.Length; i++)
			res[i] = input[i];

		for (var i = 0; i < reqPadding; i++)
			res[input.Length + i] = (byte)reqPadding;
			
		return res;
	}
}