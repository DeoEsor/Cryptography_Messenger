namespace CryptographyLib.Symmetric.AES;

public partial class Aes
{
    internal static byte[,] SubBytes(byte[,] state)
    {
        var tmp = new byte[4,4];
        for (var row = 0; row < 4; row++)
            for (var col = 0; col < Nb; col++)
                tmp[row, col] = (byte)(Sbox[state[row, col] & 0x000000ff] & 0xff);

        return tmp;
    }
    
    internal static byte[,] InvSubBytes(byte[,] state)
    {
        for (var row = 0; row < 4; row++)
            for (var col = 0; col < Nb; col++)
                state[row, col] = (byte)(InvSBox[state[row, col] & 0x000000ff] & 0xff);

        return state;
    }

    internal static byte[,] ShiftRows(byte[,] state)
    {

        var t = new byte[4];
        for (var r = 1; r < 4; r++)
        {
            for (var c = 0; c < Nb; c++)
                t[c] = state[r, (c + r) % Nb];
            for (var c = 0; c < Nb; c++)
                state[r, c] = t[c];
        }

        return state;
    }

    internal static byte[,] InvShiftRows(byte[,] state)
    {
        var t = new byte[4];
        for (var r = 1; r < 4; r++)
        {
            for (var c = 0; c < Nb; c++)
                t[(c + r) % Nb] = state[r, c];
            for (var c = 0; c < Nb; c++)
                state[r, c] = t[c];
        }
        return state;
    }

    internal static byte[,] InvMixColumns(byte[,] s)
    {
        var sp = new int[4];
        const byte b02 = 0x0e, b03 = 0x0b, b04 = 0x0d, b05 = 0x09;
        for (var c = 0; c < 4; c++)
        {
            sp[0] = FfMul(b02, s[0, c]) ^ FfMul(b03, s[1, c]) ^ FfMul(b04, s[2, c]) ^ FfMul(b05, s[3, c]);
            sp[1] = FfMul(b05, s[0, c]) ^ FfMul(b02, s[1, c]) ^ FfMul(b03, s[2, c]) ^ FfMul(b04, s[3, c]);
            sp[2] = FfMul(b04, s[0, c]) ^ FfMul(b05, s[1, c]) ^ FfMul(b02, s[2, c]) ^ FfMul(b03, s[3, c]);
            sp[3] = FfMul(b03, s[0, c]) ^ FfMul(b04, s[1, c]) ^ FfMul(b05, s[2, c]) ^ FfMul(b02, s[3, c]);
            for (var i = 0; i < 4; i++)
                s[i, c] = (byte)sp[i];
        }

        return s;
    }

    internal static byte[,] MixColumns(byte[,] s)
    {
        var sp = new int[4];
        byte b02 = 0x02, b03 = 0x03;
        for (var c = 0; c < 4; c++)
        {
            sp[0] = FfMul(b02, s[0, c]) ^ FfMul(b03, s[1, c]) ^ s[2, c] ^ s[3, c];
            sp[1] = s[0, c] ^ FfMul(b02, s[1, c]) ^ FfMul(b03, s[2, c]) ^ s[3, c];
            sp[2] = s[0, c] ^ s[1, c] ^ FfMul(b02, s[2, c]) ^ FfMul(b03, s[3, c]);
            sp[3] = FfMul(b03, s[0, c]) ^ s[1, c] ^ s[2, c] ^ FfMul(b02, s[3, c]);
            for (var i = 0; i < 4; i++)
                s[i, c] = (byte)sp[i];
        }

        return s;
    }

    internal static byte FfMul(byte a, byte b)
    {
        byte aa = a, bb = b, r = 0, t;
        while (aa != 0)
        {
            if ((aa & 1) != 0)
                r = (byte)(r ^ bb);
            t = (byte)(bb & 0x80);
            bb = (byte)(bb << 1);
            if (t != 0)
                bb = (byte)(bb ^ 0x1b);
            aa = (byte)((aa & 0xff) >> 1);
        }
        return r;
    }
}