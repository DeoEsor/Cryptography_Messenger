using System;
using System.Diagnostics;
using System.Text;
using CryptographyLib;
using System.Threading.Tasks;
using CryptographyLib.CipherModes;
using CryptographyLib.KeyExpanders;
using CryptographyLib.Paddings;
using CryptographyLib.Symmetric;
using CryptographyLib.Symmetric.AES;
using NUnit.Framework;

namespace CryptographyLib.Tests.Symmetric;

[TestFixture]
public class RijndaelTest
{
    [Test]
    public async Task Test()
    {
        /*
        Random random = new Random();
        //var generator = new FuncKeyGenerator<BigInteger>(new Func<BigInteger, byte[]>(s => s.ToByteArray()));

        (ushort)random.Next(), new Rijndael(expander), 16);
        Debug.Write(Encoding.UTF8.GetString(key));

        context.AsyncEncryptFile("Test.txt", "Encoded.txt");
        context.AsyncDecryptFile("Encoded.txt", "Decoded.txt");
        await context.AsyncEncryptFile("Test.txt", "Encoded.txt");
        await context.AsyncDecryptFile("Encoded.txt", "Decoded.txt");
        */
    }
}