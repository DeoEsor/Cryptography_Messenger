using CryptographyLib.Interfaces;

namespace CryptographyLib.Expanders;

public sealed class MagentaExpander 
    : BaseExpander
{
    public MagentaExpander(byte[] originalKey, IPadding? padding = null) 
        : base(originalKey, padding)
    {
        switch (OriginalKey.Length)
        {
            case 16: case 24:
                RoundsCount = 6;
                break;
            case 32:
                RoundsCount = 8;
                break;
            default: throw new ArgumentException($"{nameof(originalKey)} length should be 16/24/32 bytes");
        }
    }
    
    public override IEnumerator<byte[]> GetExpander()
    {
        var k1 = OriginalKey.Take(8)
            .ToArray();
        
        var k2 = OriginalKey
            .Skip(8)
            .Take(8)
            .ToArray();
        
        var k3 = OriginalKey
            .Skip(8)
            .Skip(8)
            .Take(8)
            .ToArray();
        
        var k4 = OriginalKey
            .TakeLast(8)
            .ToArray();
        
        switch (OriginalKey.Length)
        {
            case 16:
                yield return k1;
                yield return k1;
                yield return k2;
                yield return k2;
                yield return k1;
                yield return k1;
                break;
            case 24:
                yield return k1;
                yield return k2;
                yield return k3;
                yield return k3;
                yield return k2;
                yield return k1;
                break;
            case 32:
                yield return k1;
                yield return k2;
                yield return k3;
                yield return k4;
                yield return k4;
                yield return k3;
                yield return k2;
                yield return k1;
                break;
        }
    }
}