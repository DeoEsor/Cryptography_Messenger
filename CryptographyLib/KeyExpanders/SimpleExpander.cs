

// ReSharper disable MemberCanBePrivate.Global
namespace CryptographyLib.KeyExpanders;

public class SimpleExpander : BaseExpander
{
	public override int RoundsCount
	{
		get => OriginalKey.Length / BlockLength;
		protected set { ; }
	}

	public  static SimpleExpander CreateInstance(byte[] originalKey,int blockLength)
	{
		return new SimpleExpander(originalKey, blockLength);
	}
		
	public SimpleExpander(byte[] originalKey, int blockLength)
		: base(originalKey)
	{
		BlockLength = blockLength;
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