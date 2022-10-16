

// ReSharper disable MemberCanBePrivate.Global

using CryptographyLib.Interfaces;

namespace CryptographyLib.Expanders;

public class SimpleExpander : BaseExpander
{
	public  static SimpleExpander CreateInstance(byte[] originalKey,int blockLength)
	{
		return new SimpleExpander(originalKey, blockLength);
	}
		
	public SimpleExpander(byte[] originalKey, int blockLength)
		: base(originalKey)
	{
		BlockLength = blockLength;
		RoundsCount = OriginalKey.Length / BlockLength;
	}
	
	public override IEnumerator<byte[]> GetExpander()
	{
		for (var i = 0; i < OriginalKey.Length / BlockLength; i++)
		{
			if (OriginalKey
				    .Skip(i * BlockLength)
				    .Take(BlockLength)
				    .ToArray() 
			    is not { } current) 
				yield break;
			
			if (current.Length < BlockLength)
				current = Padding.ApplyPadding(current, BlockLength);
			yield return current;
		}
	}
}