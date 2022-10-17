using System.Numerics;
using CryptographyLib.Asymmetric;
using CryptographyLib.Data;
using CryptographyLib.KeyGenerators;

namespace CryptographyLib.Tests.Asymmetric;

[TestFixture]
public class ElGamalTest
{
    private AsymmetricEncryptorContext Context;
    
    [SetUp]
    public void SetUp()
    {
        var elgamal = new ElGamal
        {
            Key = new ElGamalKeyGenerator()
                .GenerateKeys()
        };
        Context = new AsymmetricEncryptorContext(CipherMode.Mode.ECB,
            0,
            elgamal,
            32);
    }

    [Test]
    public async Task Crypt()
    {
        byte[] value = new byte[4];
        TestContext.CurrentContext.Random.NextBytes(value);
        var encrypted =  await Context.EncryptAsync(value);
        var dec = await Context.DecryptAsync(encrypted);
        Assert.That(value, Is.EqualTo(dec));
    }
}