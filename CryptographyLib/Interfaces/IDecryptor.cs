namespace CryptographyLib.Interfaces;

/// <summary>
/// Interface for decryptor
/// </summary>
public interface IDecryptor
{
	/// <summary>
	/// Decryption
	/// </summary>
	/// <param name="value">Closed text</param>
	/// <returns>Open text</returns>
	byte[] Decrypt(byte[] value);
}