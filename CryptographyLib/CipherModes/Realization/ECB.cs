using CryptographyLib.Interfaces;
using CryptographyLib.KeyExpanders;
// ReSharper disable InconsistentNaming
namespace CryptographyLib.CipherModes.Realization;

public class ECB : CipherModeBase
{
	public ECB(ISymmetricEncryptor symmetricEncryptor, int BlockLength = 8)
		: base(symmetricEncryptor, BlockLength)
	{
	}

	public override byte[] Encrypt(byte[] value)
	{
		var expander = new SimpleExpander(value, BlockLength).ToList();
		var result = new List<byte[]>();
			
		Parallel.For(0,BlockLength,
			i => result[i] = Encryptor.Encrypt(expander[i]));
			
		return result.SelectMany(s => s).ToArray();
	}
		
	public override byte[] Decrypt(byte[] value)
	{
		var expander = new SimpleExpander(value, BlockLength).ToList();
		var result = new List<byte[]>();
			
		Parallel.For(0,BlockLength, 
			i => result[i] = Decryptor.Decrypt(expander[i]) );
			
		return result.SelectMany(s => s).ToArray();
	}
}