using CryptographyLib.Data;
using CryptographyLib.Data.CIpherKey;
using NumberTheory.Extensions;

namespace CryptographyLib.Extensions.CiphersExtensions.KeyExtensions;

public static class ElGamalKeyExtensions
{
    public static CipherResult<ElGamalKey> ParseElGamalKey(this Key key)
    {
        if (key.KeyType == Key.KeyTypeEnum.Symmetric)
            return new CipherResult<ElGamalKey>()
            {
                Exception = new ArgumentException( "Given symmetric key for parse ElGamal key", nameof(key))
            };
        
        var cipherResult = new CipherResult<ElGamalKey>();
        
        
        var publicValues = key
            .PublicKey
            .DeserializeBigInts();
        
        var privateValues = key
            .PrivateKey
            .DeserializeBigInts();
        
        
        if (publicValues.Length != 3)
            return new CipherResult<ElGamalKey>()
            {
                Exception = new ArgumentException("Given key is not in format for parse ElGamal key", nameof(key))
            };
        
        if (privateValues.Length != 1)
            return new CipherResult<ElGamalKey>()
            {
                Exception = new ArgumentException("Given key is not in format for parse ElGamal key", nameof(key))
            };

        cipherResult.Value = new ElGamalKey
        {
            Y = publicValues[0],
            G = publicValues[1],
            P = publicValues[2],
            X = privateValues[0]
        };

        return cipherResult;
    }
    
}