using CryptographyLib.Interfaces;

namespace CryptographyLib.Paddings;

internal class X923 : IPadding
{
	/// <inheritdoc />
	public byte[] ApplyPadding(byte[] input, int blockLength)
	{
		var reqPadding = input.Length % blockLength; 
		
		if (reqPadding == 0)
			return input;
		
		var res = new byte[input.Length + reqPadding];

		for (var i = 0; i < input.Length; i++)
			res[i] = input[i];

		for (var i = 0; i < reqPadding - 1; i++)
			res[input.Length + i] = 0;
			
		res[input.Length + reqPadding - 1] = (byte)reqPadding;
			
		return res;
	}

	/// <inheritdoc />
	public byte[] DeletePadding(byte[] input)
	{
		var reqPadding = input[^1];
		
		Array.Resize(ref input, input.Length - reqPadding);

		return input;
	}
}