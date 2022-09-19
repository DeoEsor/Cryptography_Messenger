using System;
using System.Diagnostics;
using System.Text;
using CryptographyLib.CipherModes;
using CryptographyLib.KeyExpanders;
using CryptographyLib.Paddings;
using CryptographyLib.Symmetric;
using NUnit.Framework;

namespace CryptographyLib.Tests.Symmetric;

[TestFixture]
public class DesTest
{
    [Test]
    public async Task Test()
    {
        var random = new Random();

        var key = new byte[7];
        
        TestContext.CurrentContext.Random.NextBytes(key);
        var expander = new DESExpander(key,new PKCS7());
        var context = new SymmetricEncryptorContext(CipherMode.Mode.ECB,
            (ushort)random.Next(), new DES(expander), 16);
        Debug.Write(Encoding.UTF8.GetString(key));

        await context.AsyncEncryptFile("Test.txt", "Encoded.txt");
        await context.AsyncDecryptFile("Encoded.txt", "Decoded.txt");
    }
}