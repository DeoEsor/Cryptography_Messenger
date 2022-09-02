using CryptographyLib.Interfaces;

namespace CryptographyLib.KeyGenerators;

public class ElGamalKeyGenerator : AsymmetricKeyGenerator
{
	public ElGamalKeyGenerator(IKeyGenerator privateKeyGenerator, IKeyGenerator publicKeyGenerator) 
		: base(privateKeyGenerator, publicKeyGenerator)
	{
	}
	
	
	
}