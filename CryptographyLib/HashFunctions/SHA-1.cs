using System.Collections.Specialized;
using CryptographyLib.Interfaces;
using NumberTheory.Extensions.Arithmetic;

namespace CryptographyLib.HashFunctions;

public sealed class Sha1 : ICryptoHashFunction
{
    public unsafe byte[] CreateHash(byte[] mes)
    {
        uint h0 = 0x67452301;
        var  h1 = 0xEFCDAB89;
        var  h2 = 0x98BADCFE;
        uint h3 = 0x10325476;
        var  h4 = 0xC3D2E1F0;
        var  w = stackalloc uint[80];
        
        //TODO add expander for SHA-1

        foreach (var bloc in mes)
        {
            for (var i = 16; i < 79; i++)
                w[i] = (w[i - 3] ^ w[i - 8] ^ w[i - 14] ^ w[i - 16])
                            .ShiftRotateLeft();


            var a = h0;
            var b = h1;
            var c = h2;
            var d = h3;
            var e = h4;


            for (var i = 0; i < 79; i++)
            {
                uint f = 0, k = 0;
                switch (i)
                {
                    case >= 0 and <= 19:
                        f = d ^ (b & (c ^ d)); // alternatives https://ru.wikipedia.org/wiki/SHA-1
                        k = 0x5A827999;
                        break;
                    case >= 20 and <= 39:
                        f = b ^ c ^ d;
                        k = 0x6ED9EBA1;
                        break;
                    case >= 40 and <= 59:
                        f = (b & c) | (b & d) | (c & d);
                        k = 0x8F1BBCDC;
                        break;
                    case >= 60 and <= 79:
                        f = b ^ c ^ d;
                        k = 0xCA62C1D6;
                        break;
                }

                var temp = a.ShiftRotateLeft(5) + f + e + k + w[i];
                e = d;
                d = c;
                c = b.ShiftRotateLeft(30);
                b = a;
                a = temp;   
            }
            h0 += a;
            h1 += b;
            h2 += c;
            h3 += d;
            h4 += e;
        }
        
        return BitConverter.GetBytes(h0)
            .Concat(BitConverter.GetBytes(h1))
            .Concat(BitConverter.GetBytes(h2))
            .Concat(BitConverter.GetBytes(h3))
            .Concat(BitConverter.GetBytes(h4))
            .ToArray();
    }
}