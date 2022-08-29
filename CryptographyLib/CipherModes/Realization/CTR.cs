using System.Collections;
using CryptographyLib.Extensions;
using CryptographyLib.Interfaces;
using CryptographyLib.KeyExpanders;

namespace CryptographyLib.CipherModes.Realization
{
	public class CTR : CipherModeBase
	{
		public Func<int, int> CounterCreationRule { get; set; }
		
		public CTR(long iv, Func<int, int> counterCreationRule, ISymmetricEncryptor symmetricEncryptor,
			int blockLength = 8) : base(symmetricEncryptor, blockLength)
		{
			IV = iv;
			CounterCreationRule = counterCreationRule;
		}

		public override byte[] Encrypt(byte[] value)
		{
			if (CounterCreationRule == null)
				throw new ArgumentNullException(nameof(CounterCreationRule));

			var iv = new BitArray(BitConverter.GetBytes(IV));

			var blocks = new SimpleExpander(value, BlockLength)
				.Select(s => new BitArray(s))
				.ToList();

			var result = new List<byte[]>();
			
			blocks.ForEach(s => result.Add(null!));

			Parallel.For(0, BlockLength,
				i =>
				{
					var concat = iv.Concat(BitConverter.GetBytes(CounterCreationRule(i)).ToBitArray());
					var temp = SymmetricEncryptor.Encrypt(concat.ToBytes()).ToBitArray();
					result[i] = blocks[i].Xor(temp).ToBytes();

				});

			return result.SelectMany(s => s).ToArray();
		}

		public override byte[] Decrypt(byte[] value)
		{
			if (CounterCreationRule == null)
				throw new ArgumentNullException(nameof(CounterCreationRule));

			var iv = new BitArray(BitConverter.GetBytes(IV));

			var blocks = new SimpleExpander(value, BlockLength)
				.Select(s => new BitArray(s))
				.ToList();

			var result = new List<byte[]>();
			
			blocks.ForEach(s => result.Add(null!));

			Parallel.For(0, BlockLength,
				i =>
				{
					var concat = iv.Concat(BitConverter.GetBytes(CounterCreationRule(i)).ToBitArray());
					var temp = SymmetricEncryptor.Encrypt(concat.ToBytes()).ToBitArray();
					result[i] = blocks[i].Xor(temp).ToBytes();

				});

			return result.SelectMany(s => s).ToArray();
		}
	}
}
