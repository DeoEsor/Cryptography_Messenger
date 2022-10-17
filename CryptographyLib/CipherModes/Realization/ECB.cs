using CryptographyLib.Expanders;
using CryptographyLib.Interfaces;
using CryptographyLib.KeyExpanders;
// ReSharper disable InconsistentNaming
namespace CryptographyLib.CipherModes.Realization;

public class ECB : CipherModeBase
{
	public ECB(ISymmetricEncryptor symmetricEncryptor, int BlockLength = 8)
		: base(symmetricEncryptor, BlockLength)
	{ }
	
	public ECB(IAsymmetricEncryptor asymmetric, int BlockLength = 8)
		: base(asymmetric, BlockLength)
	{ }

	public override byte[] Encrypt(byte[] value)
	{
		var expander = new SimpleExpander(value, BlockLength)
			.ToArray();

		if (value.Length < BlockLength)
			return Encryptor.Encrypt(value);
		
		var result = new byte[BlockLength * expander.Length];
			
		Parallel.For(0,expander.Length,
			i => Encryptor
							.Encrypt(expander[i])
							.CopyTo(result, BlockLength * i));
			
		return result;
	}
		
	public override byte[] Decrypt(byte[] value)
	{
		var expander = new SimpleExpander(value, BlockLength)
			.ToArray();
		
		if (value.Length < BlockLength)
			return Decryptor.Decrypt(value);
		
		var result = new byte[value.Length];
			
		Parallel.For(0,expander.Length, 
			i => Decryptor
				.Decrypt(expander[i])
				.CopyTo(result, BlockLength * i) );
			
		return result;
	}
}