using System.Numerics;
using CryptographyLib.Data;
using CryptographyLib.Interfaces;
using CryptographyLib.KeyExpanders;
using CryptographyLib.KeyGenerators;
using Microsoft.VisualBasic.CompilerServices;

namespace CryptographyLib.Asymmetric;

public sealed class Benaloh : IAsymmetricEncryptor
{
	public Benaloh(AsymmetricKeyGenerator generator, IExpandKey expandKey)
	{
		Generator = generator;
		ExpandKey = expandKey;
	}

	public byte[] Encrypt(byte[] value)
	{
		throw new NotImplementedException();
	}

	public byte[] Decrypt(byte[] value)
	{
		throw new NotImplementedException();
	}

	public AsymmetricKeyGenerator Generator { get; set; }
	public IExpandKey ExpandKey { get; set; }
}