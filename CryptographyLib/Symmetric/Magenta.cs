using CryptographyLib.KeyExpanders;

namespace CryptographyLib.Symmetric;

public class Magenta : SymmetricEncryptorBase
{
	public Magenta(IExpandKey expandKey) 
		: base(expandKey)
	{
	}

	public override byte[] Encrypt(byte[] value)
	{
		throw new NotImplementedException();
	}

	public override byte[] Decrypt(byte[] value)
	{
		throw new NotImplementedException();
	}
}