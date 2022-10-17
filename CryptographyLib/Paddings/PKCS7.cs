using CryptographyLib.Interfaces;

// ReSharper disable InconsistentNaming
namespace CryptographyLib.Paddings;

/// <summary>
/// https://datatracker.ietf.org/doc/html/rfc3369
/// </summary>
internal class PKCS7 : IPadding
{
	
	/// <inheritdoc />
	/// <seealso cref="ru.wikipedia.org/wiki/Дополнение_(криптография)#PKCS7"/>
	public byte[] ApplyPadding(byte[] input, int blockLength)
	{
		var reqPadding = blockLength - input.Length; 
		
		if (reqPadding == 0)
			return input;
		
		var res = new byte[input.Length + reqPadding];

		Array.Copy(input, res, input.Length);

		for (var i = 0; i < reqPadding; i++)
			res[input.Length + i] = (byte)reqPadding;
			
		return res;
	}

	/// <inheritdoc />
	public byte[] DeletePadding(byte[] input)
	{
		var index = input.Length - input[^1];
		var res = new byte[index];

		Array.Copy(input, res, index);
		
		return res;
	}
}