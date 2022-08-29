using System.Numerics;
using CryptographyLib.Interfaces;
using CryptographyLib.KeyExpanders;
using CryptographyLib.KeyGenerators;
using NumberTheory.Extensions;
using NumberTheory.PrimalCheckers;
using NumberTheory.RandomGenerators;


namespace CryptographyLib.Asymmetric;

public class ElGamal : IAsymmetricEncryptor
{
	public BigInteger P { get; set; }
	public BigInteger G { get; set; }
	public BigInteger Y { get; set; }
	
	private BigInteger _privateKey;

	public ElGamal(BigInteger privateKey, AsymmetricKeyGenerator generator, IExpandKey expandKey)
	{
		_privateKey = privateKey;
		Generator = generator;
		ExpandKey = expandKey;
	}

	public AsymmetricKeyGenerator Generator { get; set; }
	public IExpandKey ExpandKey { get; set; }
	
	private Random _random { get; set; } = Random.Shared;
	
	public byte[] Encrypt(byte[] value)
	{
		var res = new BigInteger[2, value.Length];
		if (value.Length <= 0) return null!;

		for (long i = 0; i <= value.Length - 1; i++)
		{
			var m = new BigInteger(value[i]);
			
			if (m <= 0) continue;
			
			var k = _random.Next() % (P - 2) +
			        1; // 1 < k < (p-1)
			var a = BigIntegerExtensions.FastPow(G, k,
				P);
			var b = BigIntegerExtensions.MultMod(BigIntegerExtensions.FastPow(Y, k, P), m, P);
			
			res[0, i] = a;
			res[1, i] = b;
		}

		return res;
	}

	public byte[] Decrypt(byte[] value)
	{
		var values = new BigInteger[2,3];
		
		var output = new List<byte>();
		if (value.Length <= 0) return null!;
		for (long i = 0; i < values[0].Length; i++)
		{
			BigInteger ai = 0;
			BigInteger bi = 0;
			var a = values[0][i];
			var b = values[1][i];

			if (a == 0 || b == 0) continue;

			var deM = BigIntegerExtensions.MultMod(b, BigIntegerExtensions.FastPow(a,
				P - 1 - _privateKey, P), P);
			output.Add(deM.ToByteArray()[0]);
		}

		return output.ToArray();
	}

	public void KeyGenerate()
	{
		var primes = new BruteForcePrimalRandomGenerator(new Solovay_StrassenTest()); //upper

		var curKey = primes.Generate(); // P
		P = curKey;//G
		
		G = primes.Generate() % (P - 1) + 1;//X
		
		_privateKey = primes.Generate() % (P - 2) + 1;//Y
		Y = BigIntegerExtensions.FastPow(G, _privateKey, P);
	}
}