namespace CryptographyLib.Interfaces;

/// <summary>
/// Interface for encryption
/// </summary>
public interface IEncryptor
{
	/// <summary>
	/// Encryption
	/// </summary>
	/// <param name="value">Open text</param>
	/// <returns>Closed text</returns>
	byte[] Encrypt(byte[] value);
}