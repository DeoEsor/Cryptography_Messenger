namespace CryptographyLib.KeyExpanders;

/// <summary>
/// Interface for Strategy Pattern for key expander
/// </summary>
public interface IExpandKey : IEnumerable<byte[]>
{
	public byte[] OriginalKey { get; set; }
	int  RoundsCount
	{
		get;
	}
}