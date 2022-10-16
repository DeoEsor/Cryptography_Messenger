using System.Collections;
using CryptographyLib.Expanders;
using CryptographyLib.Extensions.BitManipulationsExtensions;
using CryptographyLib.Interfaces;
using CryptographyLib.KeyExpanders;

namespace CryptographyLib.CipherModes.Realization;

public class OFB : CipherModeBase
{
	public OFB(ISymmetricEncryptor symmetricEncryptor,long iv, int BlockLength = 8) 
		: base(symmetricEncryptor, BlockLength)
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
			
		//Calculate all prevs and parallel
			
		foreach (var openBlock in expander)
		{
			prev = Encryptor.Encrypt(prev.ToBytes()).ToBitArray();
			result.Add(new BitArray(openBlock).Xor(prev).ToBytes());
		}
			
		return result.SelectMany(s => s).ToArray();
	}

	public override byte[] Decrypt(byte[] value)
	{
		var expander = 
			new SimpleExpander(value, value.Length / 4)
				.ToList();

		var result = new List<byte[]>();
			
		var prev = new BitArray(BitConverter.GetBytes(IV));
			
		foreach (var openBlock in expander)
		{
			prev = Encryptor.Encrypt(prev.ToBytes()).ToBitArray();
			result.Add(new BitArray(openBlock).Xor(prev).ToBytes());
		}
			
		return result.SelectMany(s => s).ToArray();
	}
}