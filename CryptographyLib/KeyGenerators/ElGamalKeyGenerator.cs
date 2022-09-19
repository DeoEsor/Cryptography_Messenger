using System.Dynamic;
using System.Numerics;
using System.Xml.Schema;
using CryptographyLib.Data;
using NumberTheory.Extensions;
using NumberTheory.PrimalCheckers;
using NumberTheory.RandomGenerators;

namespace CryptographyLib.KeyGenerators;

public class ElGamalKeyGenerator : AsymmetricKeyGenerator
{
	PrimalRandomGenerator RandomGenerator = new BruteForcePrimalRandomGenerator(new MillerRabinTest());
	public override Key GenerateKeys()
	{
		var p = RandomGenerator.Generate();
		var g = p.GetPrimalSqrt();
		var x = RandomGenerator.Generate(1, p - 1);
		var y = BigInteger.ModPow(g, x, p);
		
		return Key
			.CreateAsymmetricKey(
				new[] {y, g, p},
				new[] {x});
	}
}