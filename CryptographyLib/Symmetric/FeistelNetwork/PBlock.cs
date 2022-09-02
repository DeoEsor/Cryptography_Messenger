using System.Collections;
using CryptographyLib.Extensions;

namespace CryptographyLib.Symmetric.FeistelNetwork;

/// <summary>
/// Implementation PBlock with crypt & encrypting
/// https://www.youtube.com/watch?v=eAKi_f5Vqzo
/// </summary>
public class PBlock
{
	/// <summary>
	/// Decryption of <paramref name="value"/> with <paramref name="value"/> as P-Box 
	/// </summary>
	/// <param name="value">Encrypted value</param>
	/// <param name="pBlock">P-Box</param>
	/// <returns>Primal value</returns>
	public byte[] Decrypt(int value, byte[] pBlock) 
		=> Decrypt(BitConverter.GetBytes(value), pBlock);
		
	/// <summary>
	/// Encryption of <paramref name="value"/> with <paramref name="value"/> as P-Box 
	/// </summary>
	/// <param name="value">Value to encrypt</param>
	/// <param name="pBlock">P-Box</param>
	/// <returns>Encrypted value</returns>
	public byte[] Encrypt(int value, byte[] pBlock)
		=> Encrypt(BitConverter.GetBytes(value), pBlock);
	
	/// <inheritdoc cref="Encrypt(int,byte[])"/>
	public byte[] Encrypt(byte[] value, byte[] pBlock)
	{
		var bitValue = new BitArray(value);
		var result = new BitArray(value.Length, false);
			
		for (var i = 0; i < pBlock.Length; i++)
			result.Set(i,pBlock[i] < 32 && bitValue.Get(pBlock[i] - 1));;
		
		return result
			.ToBytes();
	}
		
	/// <inheritdoc cref="Decrypt(int,byte[])"/>
	public byte[] Decrypt(byte[] value, byte[] pBlock)
	{
		var bitValue = new BitArray(value);
		var result = new BitArray(value.Length, false);
			
		for (var i = 0; i < pBlock.Length; i++)
			result.Set(pBlock[i] - 1, bitValue[i]);;

		return result
			.ToBytes();
	}
}