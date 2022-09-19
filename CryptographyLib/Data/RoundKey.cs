namespace CryptographyLib;

[Serializable, Obsolete]
public sealed class RoundKey
{
	private readonly Lazy<byte[]> _key;

	public RoundKey(byte[] key) 
		=> _key = new Lazy<byte[]>(key);
		
	public RoundKey(Func<byte[], byte[]> f, byte[] value)
		=> _key = new Lazy<byte[]>(f.Invoke(value));

	public int KeyLength => _key.Value.Length;

	IEnumerable<byte> GetBytes() => _key.Value.AsEnumerable();

	public byte this[int index, int bit = Int32.MaxValue]
	{
		get
		{
			if (bit == Int32.MaxValue)
				return _key.Value[index];
			return _key.Value[index];// TODO
		}
	}
		
	public static implicit operator RoundKey(byte[] key) => new RoundKey(key);

	public static explicit operator byte[](RoundKey key) => key._key.Value;
}