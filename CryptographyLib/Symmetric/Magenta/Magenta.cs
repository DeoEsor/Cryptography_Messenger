using CryptographyLib.Data;
using CryptographyLib.KeyExpanders;
using NumberTheory;

namespace CryptographyLib.Symmetric;

public partial class Magenta : SymmetricEncryptorBase
{
	public Magenta(IExpandKey expandKey) 
		: base(expandKey)
	{}

	public override byte[] Encrypt(byte[] value)
    {
        var expander = new SimpleExpander(Key.SymmetricKey, 8).GetExpander();
        var res = new byte[value.Length];
        for (var i = 0; i < value.Length; i+=16)
        {
            Encoding(
                value
                    .Skip(i)
                    .Take(8)
                    .ToArray(),
                
                value
                    .Skip(i + 8)
                    .Take(8)
                    .ToArray(),
                
                expander.Current)
                .CopyTo(res, i);
            expander.MoveNext();
        }

        return res;
    }

    private byte[] Encoding(byte[] blockL, byte[] blockR, byte[] key)
    {
        var res = new byte[2, 8];
        // function V
        var tmp = new byte[8];
        var kL = new byte[8];
        var kR = new byte[8];
        
        for (var i = 0; i < 8; i++)
        {
            (blockL, blockR) = (blockR, blockL);
            for (var j = 0; j < 6; j++)
            {
                res = F(blockL, blockR, j is not (2 and 3) ? kL : kR);
                for (var g = 0; g < 8; g++)
                {
                    blockL[g] = res[0, g];
                    blockR[g] = res[1, g];
                }
            }
        }

        return blockL
            .Concat(blockR)
            .ToArray();
    }

    public override byte[] Decrypt(byte[] value) => Encrypt(value);

    #region Magenta Functions

    private static byte F(byte x) 
        => x == 255 ? (byte)0 : GaloisField.PowerGf256(0x02, x, 0x65);

    private static byte A(byte x, byte y) 
        => F((byte)(x ^ F(y)));

    private static byte[] Pe(byte x, byte y)
    {
        var result = new byte[2];
        result[0] = A(x, y);
        result[1] = A(y, x);

        return result;
    }

    private static byte[] P(byte[] x)
    {
        var res16 = new byte[16];

        for (var i = 0; i < 8; i++)
            res16[i] = Pe(x[i], x[8 + i])[i % 2];
            
        return res16;
    }

    private static byte[] T(byte[] x) 
        => P(P(P(P(x))));

    private static byte[] Xe(byte[] x)
    {
        var result = new byte[8];

        for(var i=0;i<8;i++) 
            result[i] = x[2 * i];

        return result;
    }

    private static byte[] Xo(byte[] x)
    {
        var result = new byte[8];

        for (var i = 0; i < 8; i++) 
            result[i] = x[2 * i + 1];

        return result;
    }

    private static byte[] C1(byte[] x) 
        => T(x);

    private static byte[] C2(byte[] x)
    {
        var y = new byte[16];

        for (var i = 0; i < 8; i++) 
            y[i] = (byte)(x[i] ^ Xe(C1(x))[i]);
        
        for (var i = 8; i < 16; i++) 
            y[i] = (byte)(x[i] ^ Xo(C1(x))[i - 8]);

        return T(y);
    }

    private static byte[] C3(byte[] x)
    {
        var y = new byte[16];

        for (var i = 0; i < 8; i++) 
            y[i] = (byte)(x[i] ^ Xe(C2(x))[i]);
        
        for (var i = 8; i < 16; i++) 
            y[i] = (byte)(x[i] ^ Xo(C2(x))[i - 8]);

        return T(y);
    }

    private static byte[] E(byte[] x) => Xe(C3(x));

    private static byte[,] F(byte[] xLeft, byte[] xRight, byte[] y8)
    {
        var z = new byte[2, 8];
        var xy = new byte[16];

        for (var i = 0; i < 8; i++)
        {
            xy[i] = xRight[i];
            xy[i + 8] = y8[i];
        }

        for (var i = 0; i < 8; i++)
        {
            z[0, i] = xRight[i];
            z[1, i] = (byte)(xLeft[i] ^ E(xy)[i]);
        }

        return z;
    }

    #endregion
}