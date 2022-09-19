using System;
using System.Diagnostics;
using System.Text;
using CryptographyLib;
using CryptographyLib.KeyExpanders;
using CryptographyLib.Symmetric;
using NUnit.Framework;
using CipherMode = CryptographyLib.CipherModes.CipherMode;

namespace CryptoTest.SymmetricAlgos;

[TestFixture]
public class DesTest
{
    [Test]
    public void Test()
    {
        Random random = new Random();
        //var generator = new FuncKeyGenerator<BigInteger>(new Func<BigInteger, byte[]>(s => s.ToByteArray()));
        var key = new byte[7];
        TestContext.CurrentContext.Random.NextBytes(key);
        var expander = new DesExpander(key, Padding.CreateInstance(Padding.PaddingMode.PKCS7));
        var context = new SymmetricEncryptorContext(CipherMode.Mode.ECB,
            (ushort)random.Next(), 
            new Des(expander),
            16);
        Debug.Write(Encoding.UTF8.GetString(key));

        context.AsyncEncryptFile("Test.txt", "Encoded.txt");
        context.AsyncDecryptFile("Encoded.txt", "Decoded.txt");
    }
}