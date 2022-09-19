using CryptographyLib.Data;

namespace CryptographyLib.KeyGenerators;

public abstract class AsymmetricKeyGenerator 
{
    public AsymmetricKeyGenerator()
    {
    }

    public abstract Key GenerateKeys();
}