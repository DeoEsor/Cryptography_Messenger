namespace Crypto.Domain.Data;

public sealed partial class Key
{
	public int Id { get; set; }
	public byte[] SymmetricKey { get; } = null!;

	public byte[] PublicKey { get; } = null!;

	public byte[] PrivateKey { get; } = null!;

	public KeyTypeEnum KeyType { get; }
}