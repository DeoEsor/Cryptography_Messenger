namespace CryptographyLib.Data;

public sealed class Key
{
	public enum KeyTypeEnum
	{
		Symmetric,
		Asymmetric
	}
	
	public static Key CreateSymmetricKey(byte[] symmetricKey) 
		=> new(symmetricKey);
	
	public static Key CreateAsymmetricKey(byte[] publicKey, byte[] privateKey)
		=> new(publicKey, privateKey);
	
	private Key(byte[] symmetricKey)
	{
		KeyType = KeyTypeEnum.Symmetric;
		SymmetricKey = symmetricKey;
	}
	
	private Key(byte[] publicKey, byte[] privateKey)
	{
		KeyType = KeyTypeEnum.Asymmetric;
		PublicKey = publicKey;
		PrivateKey = privateKey;
	}

	public byte[] SymmetricKey { get; } = null!;

	public byte[] PublicKey { get; } = null!;

	public byte[] PrivateKey { get; } = null!;

	public KeyTypeEnum KeyType { get; }
}