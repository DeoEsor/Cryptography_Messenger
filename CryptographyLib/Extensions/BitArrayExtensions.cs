using System.Collections;

namespace CryptographyLib.Extensions;

public static class BitArrayExtensions
{
    public static byte[] ToBytes(this BitArray bitArray)
    {
        var res = new byte[bitArray.Length / 8 + 1];
        
        bitArray.CopyTo(res,0);

        return res;
    }

    public static BitArray Concat(this BitArray bitArray, BitArray other)
    {
        var result = new BitArray(bitArray.Count + other.Count);

        var i = 0;
        foreach (var value in bitArray
                                     .Cast<bool>()
                                     .Concat(other.Cast<bool>()))
            result[i++] = value;

        return result;
    }
}