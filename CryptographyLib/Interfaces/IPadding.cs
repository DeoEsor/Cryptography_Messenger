namespace CryptographyLib.Interfaces;

/// <summary>
/// Interface for padding realizations
/// </summary>
public interface IPadding
{
	/// <summary>
	/// Padding byte array
	/// </summary>
	/// <param name="input">Byte array that should be padded</param>
	/// <param name="blockLength">Result byte array length</param>
	/// <returns>Padded byte array</returns>
	byte[] ApplyPadding(byte[] input, int blockLength);
}