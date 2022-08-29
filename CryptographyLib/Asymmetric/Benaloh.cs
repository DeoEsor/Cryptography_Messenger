using System.Numerics;
using CryptographyLib.Interfaces;
using CryptographyLib.KeyExpanders;
using CryptographyLib.KeyGenerators;
using Microsoft.VisualBasic.CompilerServices;

namespace CryptographyLib.Asymmetric;

public class Benaloh : IAsymmetricEncryptor
{
	private Keys _keys;

	public Benaloh()
	{
	}

	public Benaloh(TestType type, double minProbability, ulong size, BigInteger r)
	{
		_keys = new KeysGenerator(type, minProbability, size).Generate(r);
	}
	
	public AsymmetricKeyGenerator Generator { get; set; }
	public IExpandKey ExpandKey { get; set; }

	public BigInteger Encrypt(BigInteger message)
	{
		BigInteger u;
		while (true)
		{
			u = Utils.RandomBigInteger(2, _keys.PublicKey.n - 1);
			if (BigInteger.GreatestCommonDivisor(u, _keys.PublicKey.n) == 1)
				break;
		}

		var left = BigInteger.ModPow(_keys.PublicKey.y, message,
			_keys.PublicKey.n);
		var right = BigInteger.ModPow(u, _keys.PublicKey.r,
			_keys.PublicKey.n);
		return BigInteger.Multiply(left, right) % _keys.PublicKey.n;
	}

	public byte[] Encrypt(byte[] message)
	{
		var value = new BigInteger(message);
		
		
		while (true)
		{
			var u = Utils.RandomBigInteger(2, _keys.PublicKey.n - 1);
			if (BigInteger.GreatestCommonDivisor(u, _keys.PublicKey.n) == 1)
				break;
		}

		var left = BigInteger.ModPow(_keys.PublicKey.y, value,
			_keys.PublicKey.n);
		var right = BigInteger.ModPow(u, _keys.PublicKey.r,
			_keys.PublicKey.n);
		return (BigInteger.Multiply(left, right) % _keys.PublicKey.n).ToByteArray();
	}

	public byte[] Decrypt(byte[] value)
	{
		var message = new BigInteger(value);
		var a = BigInteger.ModPow(message, _keys.PrivateKey.f / _keys.PublicKey.r, _keys.PublicKey.n);
		
		for (var i = BigInteger.Zero; i < _keys.PublicKey.r; i++)
			if (BigInteger.ModPow(_keys.PrivateKey.x, i, _keys.PublicKey.n) == a)
				return i.ToByteArray();

		return BigInteger.MinusOne.ToByteArray();
	}
}