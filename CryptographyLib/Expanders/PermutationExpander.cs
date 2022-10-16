using CryptographyLib.Interfaces;
using CryptographyLib.Symmetric.FeistelNetwork;

namespace CryptographyLib.Expanders;

public class PermutationExpander : BaseExpander
{
    private PBlock _pBlock = new PBlock();
    private byte[] _pBlockRule;
    public Lazy<byte[]> PermutedKey { get; }
    
    public PermutationExpander(byte[] originalKey, byte[] pBlockRule, IPadding padding = null!)
        : base(originalKey, padding)
    {
        _pBlockRule = pBlockRule;
        PermutedKey = new Lazy<byte[]>(_pBlock.Encrypt(originalKey, pBlockRule));
    }
    
    public override IEnumerator<byte[]> GetExpander()
    {
        for (var i = 0; i < OriginalKey.Length / BlockLength; i++)
        {
            if (PermutedKey.Value
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