using System.Diagnostics.CodeAnalysis;
using CryptographyLib.Interfaces;
using CryptographyLib.KeyExpanders;
using CryptographyLib.KeyGenerators;


namespace CryptographyLib.Asymmetric;

public sealed class ElGamal : IAsymmetricEncryptor
{
	public ElGamal(AsymmetricKeyGenerator generator, IExpandKey expandKey)
	{
		Generator = generator;
		ExpandKey = expandKey;
	}

	public byte[] Encrypt(byte[] value)
	{
		throw new NotImplementedException();
	}

	public byte[] Decrypt([NotNull]byte[] value)
	{
		throw new NotImplementedException();
	}

	public AsymmetricKeyGenerator Generator { get; set; }
	public IExpandKey ExpandKey { get; set; }
}