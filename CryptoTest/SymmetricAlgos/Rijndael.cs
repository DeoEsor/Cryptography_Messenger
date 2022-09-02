using System;
using System.Diagnostics;
using System.Text;
using CryptographyLib.CipherModes;
using CryptographyLib.KeyExpanders;
using CryptographyLib.Paddings;
using CryptographyLib.Symmetric;
using CryptographyLib.Symmetric.AES;
using NUnit.Framework;

namespace CryptoTest.SymmetricAlgos;

[TestFixture]
public class RijndaelTest
{
    [Test]
    public void Test()
    {
        Random random = new Random();
        //var generator = new FuncKeyGenerator<BigInteger>(new Func<BigInteger, byte[]>(s => s.ToByteArray()));
        var key = new byte[7];
        TestContext.CurrentContext.Random.NextBytes(key);
        var expander = new DESExpander(key,new PKCS7());
        var context = new SymmetricEncryptorContext(CipherMode.Mode.ECB,
            (ushort)random.Next(), new Rijndael(expander), 16);
        Debug.Write(Encoding.UTF8.GetString(key));

        context.AsyncEncryptFile("Test.txt", "Encoded.txt");
        context.AsyncDecryptFile("Encoded.txt", "Decoded.txt");
    }
}