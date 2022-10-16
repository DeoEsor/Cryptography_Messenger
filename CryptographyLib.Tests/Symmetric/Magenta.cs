using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using CryptographyLib.Expanders;

namespace CryptographyLib.Tests.Symmetric;

[TestFixture]
public class Magenta
{
    private byte[] Key;
    private SymmetricEncryptorContext Context;
    
    [SetUp]
    public void SetUp()
    {
        Key = new byte[32];
        
        var expander = new MagentaExpander(Key);
        
        TestContext.CurrentContext.Random.NextBytes(Key);
        
        Context = new SymmetricEncryptorContext(CipherMode.Mode.ECB,
            TestContext.CurrentContext.Random.NextUShort(), 
            new CryptographyLib.Symmetric.Magenta(expander),
            16);
    }

    [Test]
    public async Task Crypt()
    {
        await Context.AsyncEncryptFile("Test.txt", "Encoded.txt");
        await Context.AsyncDecryptFile("Encoded.txt", "Decoded.txt");
    }
}