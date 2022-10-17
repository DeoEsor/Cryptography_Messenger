using System.Numerics;
using CryptographyLib.Data;
using CryptographyLib.Extensions.CiphersExtensions.KeyExtensions;
using CryptographyLib.Interfaces;
using CryptographyLib.KeyExpanders;
using CryptographyLib.KeyGenerators;
using NumberTheory.Extensions;
using NumberTheory.PrimalCheckers;
using NumberTheory.RandomGenerators;


namespace CryptographyLib.Asymmetric;

public sealed class ElGamal : IAsymmetricEncryptor
{
	public AsymmetricKeyGenerator Generator { get; set; } = new ElGamalKeyGenerator();

	public IExpandKey ExpandKey { get; set; }
	
	public PrimalRandomGenerator Random { get; set; } = new BruteForcePrimalRandomGenerator( new MillerRabinTest());
	
	public Key Key { get; set; }
	
	public byte[] Encrypt(byte[] value)
	{
		Key ??= Generator.GenerateKeys();
		
		var keyResult = Key.ParseElGamalKey();
		if (!keyResult.IsSuccessful)
		{
			// TODO
			// ignored
		}

		var elgamalKey = keyResult.Value;
		var p = elgamalKey.P;
		var g = elgamalKey.G;
		var y = elgamalKey.Y;
		
		var m = new BigInteger(value);

		
		var k = Random.Generate(3, p - BigInteger.One);

		while (BigInteger.GreatestCommonDivisor(k, p - BigInteger.One) != 1)
			k = Random.Generate(1, p - BigInteger.One);
		
		var a = BigInteger.ModPow(g , k , p);
		
		var b = y.FastPow(k, p) * m % p;
		

		return new[] { a, b }
			.SerializeBigInts();
	}

	public byte[] Decrypt(byte[] value)
	{
		Key ??= Generator.GenerateKeys();
		var nums = value.DeserializeBigInts();
		var keyResult = Key.ParseElGamalKey();
		if (!keyResult.IsSuccessful)
		{
			// TODO
			// ignored
		}

		var elgamalKey = keyResult.Value;
		var p = elgamalKey.P;
		var x = elgamalKey.X;
		
		BigInteger a = nums[0], b = nums[1];

		var m = BigInteger
			        .Multiply(b, a.FastPow(p - BigInteger.One - x, p)) % p;

		return m
			.ToByteArray();
	}
}