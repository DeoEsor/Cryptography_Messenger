using CryptographyLib.KeyExpanders;
using NumberTheory;

namespace CryptographyLib.Symmetric;

public sealed partial class Magenta : SymmetricEncryptorBase
{
	public Magenta(IExpandKey expandKey) 
		: base(expandKey)
	{}

	public override byte[] Encrypt(byte[] value)
    {
        var expander = 
            new SimpleExpander(Key.SymmetricKey, 8)
            .GetExpander();
        
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
        var kL = key
            .Take(8)
            .ToArray();
        
        var kR = key
            .TakeLast(8)
            .ToArray();
        
        for (var i = 0; i < 6; i++)
        {
            var res = F(blockL, blockR, i is not (2 or 3) ? kL : kR);
            
            for (var g = 0; g < 8; g++)
                (blockL[g], blockR[g]) = (res[0, g], res[1, g]);
            
            if (i != 5)
                (blockL, blockR) = (blockR, blockL);
        }

        return blockL
            .Concat(blockR)
            .ToArray();
    }

    public override byte[] Decrypt(byte[] value) => Encrypt(value);
}