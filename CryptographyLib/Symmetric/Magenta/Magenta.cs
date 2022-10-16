using CryptographyLib.KeyExpanders;

namespace CryptographyLib.Symmetric;

public sealed partial class Magenta : SymmetricEncryptorBase
{
    public Magenta(IExpandKey expandKey) 
        : base(expandKey)
    {}

    public override byte[] Encrypt(byte[] value)
    {
        var expander = ExpandKey.ToArray();
        var res = new byte[value.Length];
        var count = value.Length / 16;
        if (count == 0)
            count = 1;

        Parallel.For(0, count, 
            i => 
                Encoding(
                value.AsSpan(i, 8),
                value.AsSpan(i + 8, 8),
                expander[i])
            .CopyTo(res, i));

        return res;
    }

    private byte[] Encoding(Span<byte> blockL, Span<byte> blockR, byte[] key)
    {
        for (var i = 0; i < 6; i++)
        {
            var res = F(blockL, blockR, key);
            
            for (var g = 0; g < 8; g++)
                (blockL[g], blockR[g]) = (res[0, g], res[1, g]);

            if (i == 5) 
                continue;
            
            var tmp = blockL;
            blockL = blockR;
            blockR = tmp;

        }

        var result = new byte[blockL.Length + blockR.Length];
        
        for (int i = 0; i < blockL.Length; i++)
        {
            result[i] = blockL[i];
            result[i + 8] = blockR[i];
        }

        return result;
    }

    public override byte[] Decrypt(byte[] value) 
        => Encrypt(value);
}