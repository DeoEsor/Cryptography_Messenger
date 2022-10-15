// ReSharper disable InconsistentNaming

using System.Diagnostics.CodeAnalysis;
using CryptographyLib.Interfaces;
using NumberTheory.Extensions.Arithmetic;
namespace CryptographyLib.Paddings;

internal class ISO_10126 : IPadding
{
	/// <inheritdoc />
	public byte[] ApplyPadding(byte[] input, int blockLength)
	{
		var random = new Random();
		
		var reqPadding = blockLength - input.Length; 
		
		if (reqPadding == 0)
			return input;
		
		var res = new byte[input.Length + reqPadding];

		for (var i = 0; i < input.Length; i++)
			res[i] = input[i];

		for (var i = 0; i < reqPadding - 1; i++)
			res[input.Length + i] = (byte)random.Next(0, 15);
		
		res[^1] = (byte)reqPadding;
		
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