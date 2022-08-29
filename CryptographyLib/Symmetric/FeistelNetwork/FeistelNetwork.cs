using System.Collections;
using CryptographyLib.Extensions;
using CryptographyLib.Interfaces;
using CryptographyLib.KeyExpanders;
using CryptographyLib.Paddings;
namespace CryptographyLib.Symmetric.FeistelNetwork
{
	/// <summary>
	/// 
	/// </summary>
	public class FeistelNetwork : SymmetricEncryptorBase
	{
		private static readonly byte[] E = new byte[]
		{
				32,	1,	2,	3,	4,	5,
				4,	5,	6,	7,	8,	9,
				8,	9,	10,	11,	12,	13,
				12,	13,	14,	15,	16,	17,
				16,	17,	18,	19,	20,	21,
				20,	21,	22,	23,	24,	25,
				24,	25,	26,	27,	28,	29,
				28,	29,	30,	31,	32,	1
		};
		private readonly ISymmetricEncryptor _encryptor;
		public int Rounds
		{
			get;
			set;
		}
		private IPadding Padding = new PKCS7();

		public FeistelNetwork(  IExpandKey expandKey,
								ISymmetricEncryptor symmetricEncryptor = null!)
			: base(expandKey)
		{
			_encryptor = symmetricEncryptor;
			Rounds = ExpandKey.RoundsCount;
		}

		public override byte[] Encrypt(byte[] value)
		{
			value = Padding.ApplyPadding(value, 4);
			BitArray l = new BitArray(Enumerable.Take(value, value.Length / 2).ToArray()), 
				   r = new BitArray(Enumerable.TakeLast(value, value.Length / 2).ToArray());

			for (var i = 0; i < Rounds - 1; i++)
			{
				r = r.Xor(new BitArray(_encryptor.Encrypt(l.ToBytes())));
				(l, r) = (r, l);
			}
			r = r.Xor(new BitArray(_encryptor.Encrypt(l.ToBytes())));
			byte[] res = new byte[r.Length / 8 + 1];
			r.CopyTo(res,0);
			return res.ToArray();
		}

		public override byte[] Decrypt(byte[] value)
		{
			value = Padding.ApplyPadding(value, 4);
			byte[]  l = Enumerable.Take(value, value.Length / 2).ToArray(), 
					r = Enumerable.TakeLast(value, value.Length / 2).ToArray();

			for (var i = Rounds - 1; i > 0; i++)
			{
				r = r.XorBytes(_encryptor.Decrypt(l));
				(l, r) = (r, l);
			}
			
			r = r.XorBytes(_encryptor.Decrypt(l));
			var res = l.ToList();
			res.AddRange(r); 
			return res.ToArray();
		}
	}
}
