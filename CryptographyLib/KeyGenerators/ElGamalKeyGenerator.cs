using System.Numerics;
using CryptographyLib.Data;
using NumberTheory.Extensions;
using NumberTheory.PrimalCheckers;
using NumberTheory.RandomGenerators;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace CryptographyLib.KeyGenerators;

public class ElGamalKeyGenerator : AsymmetricKeyGenerator
{
	readonly PrimalRandomGenerator _randomGenerator = new BruteForcePrimalRandomGenerator(new MillerRabinTest());
	public override Key GenerateKeys()
	{
		var p = _randomGenerator.Generate(int.MaxValue, long.MaxValue );
		var g = p.GetPrimalSqrt();
		var x = _randomGenerator.Generate(1, p - 1);
		var y = BigInteger.ModPow(g, x, p);
		
		return Key
			.CreateAsymmetricKey(
				new[] {y, g, p},
				new[] {x});
	}
}