using CryptographyLib.Data;
using CryptographyLib.Interfaces;
using CryptographyLib.KeyExpanders;

namespace CryptographyLib.Symmetric;

public abstract class SymmetricEncryptorBase : ISymmetricEncryptor
{
	private IExpandKey _expandKey;
	
	protected Key Key { get; set; }
		
	protected SymmetricEncryptorBase(IExpandKey expandKey)
	{
		_expandKey = expandKey;
	}
		
	public IExpandKey ExpandKey
	{
		get => _expandKey;
	}

	public abstract byte[] Encrypt(byte[] value);

	public abstract byte[] Decrypt(byte[] value);

	protected virtual byte[] EncryptRound(byte[] value)
	{
		throw new NotImplementedException();
	}

	protected virtual byte[] DecryptRound(byte[] value)
	{
		throw new NotImplementedException();
	}
}