namespace CryptographyLib.Interfaces;

public interface IAsyncDecryptor
{
	/// <summary>
	/// Decryption
	/// </summary>
	/// <param name="value">Closed text</param>
	/// <param name="originalKey">key</param>
	/// <returns>Open text</returns>
	Task<byte[]> AsyncDecrypt(Stream value, byte[] originalKey);
}