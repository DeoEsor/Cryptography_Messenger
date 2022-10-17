using System.Collections;
using CryptographyLib.Expanders;
using CryptographyLib.Extensions.BitManipulationsExtensions;
using CryptographyLib.Interfaces;
using CryptographyLib.KeyExpanders;
namespace CryptographyLib.CipherModes.Realization;

public class CBC : CipherModeBase
{
	private ulong _iv;

	public CBC(ISymmetricEncryptor encryptor, ulong iv, int blocksCount) 
		: base(encryptor, blocksCount)
	{
		_iv = iv;
	}
	
	public CBC(IAsymmetricEncryptor encryptor, ulong iv, int blocksCount) 
		: base(encryptor, blocksCount)
	{
		_iv = iv;
	}
		
	public override byte[] Encrypt(byte[] value)
	{
		var expander = 
			new SimpleExpander(value, value.Length / 4)
				.ToList();
			
		var result = new List<byte[]>();
		var ivVector = new BitArray(BitConverter.GetBytes(_iv));
		BitArray resultBlock = null!;

		foreach (var block in expander)
		{
			var temp = new BitArray(block);
			var xor = resultBlock == null ? ivVector.Xor(temp) : resultBlock.Xor(temp);
			resultBlock = Encryptor
				.Encrypt(xor.ToBytes())
				.ToBitArray();
			result.Add(resultBlock.ToBytes());
		}

		return result.SelectMany(s => s).ToArray();
	}

	public override byte[] Decrypt(byte[] value)
	{
		var expander = 
			new SimpleExpander(value, value.Length / 4)
				.ToList();
			
		var result = new List<byte[]>();
		var ivVector = new BitArray(BitConverter.GetBytes(_iv));
		BitArray resultBlock = null!;

		foreach (var block in expander)
		{
			var temp = new BitArray(block);
			var xor = resultBlock == null ? ivVector.Xor(temp) : resultBlock.Xor(temp);
			resultBlock = Decryptor
				.Decrypt(xor.ToBytes())
				.ToBitArray();
			result.Add(resultBlock.ToBytes());
		}

		return result.SelectMany(s => s).ToArray();
	}
}