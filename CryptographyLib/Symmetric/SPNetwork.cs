using CryptographyLib.KeyExpanders;
using CryptographyLib.Symmetric.FeistelNetwork;
namespace CryptographyLib.Symmetric.SPNetwork
{
	public class SPNetwork : SymmetricEncryptorBase
	{

		private SBlock SBlock = new SBlock();
		private PBlock PBlock = new PBlock();
		public SPNetwork(IExpandKey expandKey) 
			: base(expandKey) {}

		public override byte[] Encrypt(byte[] value)
		{
			foreach (var roundKey in ExpandKey)
				value = EncryptRound(value);
			return value;
		}

		public override byte[] Decrypt(byte[] value)
		{
			foreach (var roundKey in ExpandKey)
				value = DecryptRound(value);
			return value;
		}

		protected override byte[] DecryptRound(byte[] value)
		{
			var expandValue = new SimpleExpander(value, 4);
			using var roundKeys = ExpandKey.GetEnumerator();
			var sblockRes = new List<byte>();

			foreach (byte[] partValue in expandValue)
			{
				sblockRes.AddRange( SBlock.Encrypt(partValue, roundKeys.Current));
				if (!roundKeys.MoveNext())
					break;
			}

			byte[] a = null!;
			
			if (roundKeys.Current != null)
				a = PBlock.Decrypt(BitConverter.ToInt32(sblockRes.ToArray()), roundKeys.Current);
			
			return a;
		}
		protected override byte[] EncryptRound(byte[] value)
		{
			var expandValue =new SimpleExpander(value, 4);
			var sblockRes = new List<byte>();
			
			using var roundKeys = ExpandKey.GetEnumerator();

			foreach (byte[] partValue in expandValue)
			{
				sblockRes.AddRange( SBlock.Encrypt(partValue, roundKeys.Current));
				if (!roundKeys.MoveNext())
					break;
			}

			byte[] a = null!;
			
			if (roundKeys.Current != null)
				a = PBlock.Decrypt(sblockRes.ToArray(), roundKeys.Current);
			
			return a;
		}
		
	}
}
