using System.Numerics;
using CryptographyLib.Interfaces;
using CryptographyLib.KeyExpanders;
using CryptographyLib.KeyGenerators;

namespace CryptographyLib.Asymmetric.RSA;

public class RSA : IAsymmetricEncryptor
{
    public AsymmetricKeyGenerator Generator { get; set; }
    public IExpandKey ExpandKey { get; set; }

    public RSA(IExpandKey expandKey, RSAKeyGenerator generator = null!)
    {
        ExpandKey = expandKey;
        Generator = generator ?? new RSAKeyGenerator(65537);
    }
    
    public byte[] Encrypt(byte[] value)
    {
        var text = new BigInteger(value);
        var e = Generator
            .CreatePublicKey()
            .Take(Generator.CreatePublicKey().Length / 2)
            .ToArray();
        var n = Generator
            .CreatePublicKey()
            .TakeLast(Generator.CreatePublicKey().Length / 2)
            .ToArray();

        return BigInteger.ModPow(text,new BigInteger(e), new BigInteger(n)).ToByteArray();
    }

    public byte[] Decrypt(byte[] value)
    {
        var text = new BigInteger(value);
        var d = Generator
            .CreatePrivateKey()
            .Take(Generator.CreatePublicKey().Length / 2)
            .ToArray();
        var n = Generator
            .CreatePrivateKey()
            .TakeLast(Generator.CreatePublicKey().Length / 2)
            .ToArray();

        return BigInteger.ModPow(text,new BigInteger(d), new BigInteger(n)).ToByteArray();
    }

}