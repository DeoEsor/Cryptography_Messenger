// ReSharper disable InconsistentNaming

using System.Collections;
using CryptographyLib.Extensions;
using CryptographyLib.Extensions.BitManipulationsExtensions;

namespace CryptographyLib.Symmetric.FeistelNetwork;

/// <summary>
/// Implementation PBlock with very very slow crypt & encrypting
/// https://www.youtube.com/watch?v=eAKi_f5Vqzo
/// </summary>
public class SBlock
{
	/// <summary>
	/// S-Box implementation slowly than another variants, but closer to be true
	/// </summary>
	/// <param name="value">Byte array of value</param>
	/// <param name="SBlock">Rule for substitution</param>
	/// <param name="k">Length of input bitarray to be substituted</param>
	/// <returns>Substituted byte array</returns>
	/// <exception cref="ArgumentException">SBlock invalid or value wasn't be padded</exception>
	public byte[] Encrypt(byte[] value, Dictionary<BitArray, BitArray> SBlock, int k)
	{
		if (false)
		{
			// Todo check correct arguments
			throw new ArgumentException("");
		}

		var copy = new BitArray(value);

		var bitArray = new BitArray[copy.Length / k];

		for (int i = 0; i < copy.Length / k; i++)
		{
			bitArray[i] = new BitArray(k);
				
			for (var j = 0; j < k; j++)
				bitArray[i].Set(j, copy[i * k + j]);
		}

		for (var i = 0; i < bitArray.Length; i++)
		{
			if (!SBlock.ContainsKey(bitArray[i])) throw new ArgumentException();

			bitArray[i] = SBlock[bitArray[i]];
		}


		var byteResult = new byte[(bitArray.Sum( s=> s.Length) - 1) / 8 + 1];
		var shift = 0;
		foreach (var t in bitArray)
		{
			t.CopyTo(byteResult, shift);
			shift += (t.Length - 1 )/ 8 + 1;
		}

		return byteResult;
	}

	#region Incorrect Implementation
	/// <summary>
	/// Encryption
	/// </summary>
	/// <param name="value">bytes array</param>
	/// <param name="SBlock">Rule for creating SBlock</param>
	/// <param name="k">size of SBlock</param>
	/// <returns>Encrypted bytes array</returns>
	public byte[] Encrypt(int value, Dictionary<byte, byte> SBlock, int k)
	{
		var i = value.CountOfBites() / 4;

		if (i == 0) i = 1;

		return value.GetBytes().ToArray();
	}

	/// <inheritdoc cref="Encrypt(int,System.Collections.Generic.Dictionary{byte,byte},int)"/>
	public byte[] Encrypt(byte[] value, Dictionary<byte, byte> SBlock, int k)
	{
		var result = new byte[value.Length];

		for (var i = 0; i < value.Length; i++)
			result[i] = (byte)(SBlock[value[i]] << i != 1 ? i-- : i);

		return result;
	}

	public  byte[] Encrypt(byte[] value, byte[] key)
	{
		var result = new byte[value.Length];

		for (var i = 0; i < value.Length; i++)
			result[i] = (byte)(value[value[i]]);

		return result;
	}

	/// <inheritdoc cref="Encrypt(int,System.Collections.Generic.Dictionary{byte,byte},int)"/>
	public  byte[] Encrypt(int value, Func<byte, byte> SBlock, int k)
		=> Encrypt(value, SBlock.CreateKeyByFunc(k), k);

	/// <inheritdoc cref="Encrypt(int,System.Collections.Generic.Dictionary{byte,byte},int)"/>
	public  byte[] Encrypt(byte[] value, Func<byte, byte> SBlock, int k)
		=> Encrypt(value, SBlock.CreateKeyByFunc(k), k);

	/// <summary>
	/// Decryption
	/// </summary>
	/// <param name="value">bytes array</param>
	/// <param name="SBlock">Rule for creating SBlock</param>
	/// <param name="k">size of SBlock</param>
	/// <returns>Original bytes array</returns>
	public  byte[] Decrypt(byte[] value, Func<byte, byte> SBlock, int k)
		=> Decrypt(value, SBlock.CreateKeyByFunc(k), k);

	/// <inheritdoc cref="Decrypt(byte[],System.Collections.Generic.Dictionary{byte,byte},int)"/>
	public byte[] Decrypt(byte[] value, Dictionary<byte, byte> SBlock, int k)
	{
		var res
			= SBlock
				.GroupBy(p => p.Value)
				.ToDictionary(
					g => g.Key,
					g => g.Select(pp => pp.Key)
						.First());

		return Encrypt(value, res, k);
	}
		
	public  byte[] Decrypt(byte[] value, byte[] key)
	{
		var result = new byte[value.Length];

		for (var i = 0; i < value.Length; i++)
			result[i] = (byte)(value[value[i]]);

		return result;
	}

	#endregion
}