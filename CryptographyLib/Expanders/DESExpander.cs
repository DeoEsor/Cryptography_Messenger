using CryptographyLib.Interfaces;

namespace CryptographyLib.Expanders;

public class DesExpander : BaseExpander
{
    public DesExpander(byte[] originalKey, IPadding padding = null!) 
        : base(originalKey, padding)
    {
        RoundsCount = 16;
        BlockLength = 6; // 6 * 8 = 48 bit vector
    }
    
    public override IEnumerator<byte[]> GetExpander()
    {
        for (var i = 0; i < OriginalKey.Length / BlockLength; i++)
        {
            if (OriginalKey
                    .Skip(i * BlockLength)
                    .Take(BlockLength) 
                is not byte[] current) 
                yield break;
            if (current.Length < BlockLength)
                Padding?.ApplyPadding(current, BlockLength);
            yield return current;
        }
    }
}