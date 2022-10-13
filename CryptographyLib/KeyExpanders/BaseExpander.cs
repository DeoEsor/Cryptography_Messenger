using System.Collections;
using CryptographyLib.Paddings;
// ReSharper disable MemberCanBePrivate.Global
namespace CryptographyLib.KeyExpanders;

public abstract class BaseExpander : IExpandKey
{
	private byte[] _originalKey;
	protected IPadding Padding; 
		
		
	public byte[] OriginalKey
	{
		get => _originalKey;
		set => _originalKey = value;
	}
	public abstract int RoundsCount
	{
		get;
		protected set;
	}
		
	public int BlockLength { get; protected init; }

	protected BaseExpander(byte[] originalKey, IPadding padding = null!)
	{
		_originalKey = originalKey;
		Padding = padding ?? new PKCS7();
	}

	IEnumerator<byte[]> IEnumerable<byte[]>.GetEnumerator()
		=> GetExpander();

	public IEnumerator GetEnumerator() => GetExpander();
		
	public abstract IEnumerator<byte[]> GetExpander();
}