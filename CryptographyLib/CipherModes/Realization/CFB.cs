using System.Collections;
using CryptographyLib.Extensions;
using CryptographyLib.Interfaces;
using CryptographyLib.KeyExpanders;
namespace CryptographyLib.CipherModes.Realization;

public class CFB : CipherModeBase
{
	public CFB(ISymmetricEncryptor symmetricEncryptor,long iv, int blockLength = 8)
		: base(symmetricEncryptor, blockLength)
	{
		IV = iv;
	}

	public override byte[] Encrypt(byte[] value)
	{
		var expander = 
			new SimpleExpander(value, value.Length / 4)
				.ToList();

		var result = new List<byte[]>();
			
		var prev = new BitArray(BitConverter.GetBytes(IV));
			
		foreach (var openBlock in expander)
		{
			var c = Encryptor.Encrypt(prev.ToBytes());
			prev = new BitArray(openBlock).Xor(c.ToBitArray());
			result.Add(prev.ToBytes());
		}
			
		return result.SelectMany(s => s).ToArray();
	}
	public override byte[] Decrypt(byte[] value)
	{ //TODO Parallel
		var expander = 
			new SimpleExpander(value, value.Length / 4)
				.ToList();

		var result = new List<byte[]>();
			
		var prev = new BitArray(BitConverter.GetBytes(IV));
			
		foreach (var openBlock in expander)
		{
			var c = Encryptor.Encrypt(prev.ToBytes());
			prev = new BitArray(openBlock).Xor(c.ToBitArray());
			result.Add(prev.ToBytes());
		}
			
		return result.SelectMany(s => s).ToArray();
	}
}