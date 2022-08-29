using System.Collections;
using CryptographyLib.Extensions;
using CryptographyLib.Interfaces;
using CryptographyLib.KeyExpanders;

namespace CryptographyLib.CipherModes.Realization
{
	public class Ofb : CipherModeBase
	{
		public Ofb(ISymmetricEncryptor symmetricEncryptor,long iv, int blockLength = 8) 
			: base(symmetricEncryptor, blockLength)
		{
			Iv = iv;
		}

		public override byte[] Encrypt(byte[] value)
		{
			var expander = 
				new SimpleExpander(value, value.Length / 4)
					.ToList();

			var result = new List<byte[]>();
			
			var prev = new BitArray(BitConverter.GetBytes(Iv));
			
			//Calculate all prevs and parallel
			
			foreach (var openBlock in expander)
			{
				prev = Encryptor.Encrypt(prev.ToBytes()).ToBitArray();
				result.Add(new BitArray(openBlock).Xor(prev).ToBytes());
			}
			
			return result.SelectMany(s => s).ToArray();
		}

		public override byte[] Decrypt(byte[] value)
		{
			var expander = 
				new SimpleExpander(value, value.Length / 4)
					.ToList();

			var result = new List<byte[]>();
			
			var prev = new BitArray(BitConverter.GetBytes(Iv));
			
			foreach (var openBlock in expander)
			{
				prev = Encryptor.Encrypt(prev.ToBytes()).ToBitArray();
				result.Add(new BitArray(openBlock).Xor(prev).ToBytes());
			}
			
			return result.SelectMany(s => s).ToArray();
		}
	}
}
