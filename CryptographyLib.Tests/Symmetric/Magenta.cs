namespace CryptographyLib.Tests.Symmetric;

[TestFixture]
public class Magenta
{
    [Test]
    public async Task Test()
    {
        var random = new Random();

        var key = new byte[7];
        
        TestContext.CurrentContext.Random.NextBytes(key);
        var expander = new DesExpander(key, Padding.CreateInstance(Padding.PaddingMode.PKCS7));
        var context = new SymmetricEncryptorContext(CipherMode.Mode.ECB,
            (ushort)random.Next(), 
            new Des(expander),
            16);
        Debug.Write(Encoding.UTF8.GetString(key));

        await context.AsyncEncryptFile("Test.txt", "Encoded.txt");
        await context.AsyncDecryptFile("Encoded.txt", "Decoded.txt");
    }
}