// ReSharper disable MemberCanBePrivate.Global
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
		var count = OriginalKey.Length / BlockLength;

		if (OriginalKey.Length % BlockLength != 0)
			count++;
		
		for (var i = 0; i < count; i++)
		{
			var current = OriginalKey
				.Skip(i * BlockLength)
				.Take(BlockLength)
				.ToArray();
			
			if (current.Length < BlockLength)
				current = Padding.ApplyPadding(current, BlockLength);
			yield return current;
		}
	}
}