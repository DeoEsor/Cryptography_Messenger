using CryptographyLib.Interfaces;
using CryptographyLib.KeyExpanders;
using CryptographyLib.KeyGenerators;


namespace CryptographyLib.Asymmetric;

public class ElGamal : IAsymmetricEncryptor
{
	public AsymmetricKeyGenerator Generator { get; set; }
	
	public IExpandKey ExpandKey { get; set; }
	
	public byte[] Encrypt(byte[] value)
	{
		throw new NotImplementedException();
	}

	public byte[] Decrypt(byte[] value)
	{
		throw new NotImplementedException();
	}
}