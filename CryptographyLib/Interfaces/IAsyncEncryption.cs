namespace CryptographyLib.Interfaces;

public interface IAsyncEncryptor
{
	/// <summary>
	/// Encryption
	/// </summary>
	/// <param name="value">Open text</param>
	/// <param name="originalKey">key</param>
	/// <returns>Closed text</returns>
	Task<byte[]> AsyncEncrypt(Stream value, byte[] originalKey);
}