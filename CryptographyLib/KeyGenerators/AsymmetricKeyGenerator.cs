using CryptographyLib.Data;
using CryptographyLib.Interfaces;

namespace CryptographyLib.KeyGenerators;

public abstract class AsymmetricKeyGenerator 
{
    public AsymmetricKeyGenerator()
    {
    }

    public abstract Key GenerateKeys();
}