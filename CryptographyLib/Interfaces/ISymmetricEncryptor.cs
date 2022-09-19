using CryptographyLib.KeyExpanders;

namespace CryptographyLib.Interfaces;

/// <summary>
/// Interface of Symmetric Encryptor
/// </summary>
public interface ISymmetricEncryptor : IEncryptor, IDecryptor
{	
	/// <summary>
	/// Rule of getting round keys
	/// </summary>
	IExpandKey ExpandKey { get; }
}