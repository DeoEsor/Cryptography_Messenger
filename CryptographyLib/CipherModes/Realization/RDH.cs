using System.Collections;
using System.Security.Authentication;
using CryptographyLib.Expanders;
using CryptographyLib.Extensions.BitManipulationsExtensions;
using CryptographyLib.Interfaces;
using CryptographyLib.KeyExpanders;
using NUnit.Framework;

namespace CryptographyLib.CipherModes.Realization;

public class RDH : CipherModeBase
{
	public byte[] Hash { get; set; }
	public ulong Delta { get; set; }

	public RDH(ISymmetricEncryptor symmetricEncryptor, long iv, byte[] hash, int BlockLength = 8,
		ulong rd = UInt64.MinValue)
		: base(symmetricEncryptor, BlockLength)
	{
		IV = iv;
		Delta = rd == UInt64.MinValue ? TestContext.CurrentContext.Random.NextULong() : rd;
		if (hash.Length == 128)
			Hash = hash;
		/*
		 * if (Delta % 2 == 0)
		 *		Delta += 1;
		 */
	}

	public RDH(IAsymmetricEncryptor asymmetricEncryptor, long iv, byte[] hash,  int BlockLength = 8, ulong rd = UInt64.MinValue)
			: base(asymmetricEncryptor, BlockLength)
		{
			IV = iv;
			Delta = rd == UInt64.MinValue ? TestContext.CurrentContext.Random.NextULong() : rd;
			if (hash.Length == 128)
				Hash = hash;
			/*
			 * if (Delta % 2 == 0)
			 *		Delta += 1;
			 */
		}
		
	public override byte[] Encrypt(byte[] value)
	{
		var iv = new BitArray(BitConverter.GetBytes(IV));
		var hash = new BitArray(Hash);

		var blocks = new SimpleExpander(value, BlockLength)
			.Select(s => new BitArray(s))
			.ToList();

		var result = new List<byte[]>();
		result.AddRange(new List<byte[]>(){null!, null!}); // for iv & hash
			
		blocks.ForEach(_ => result.Add(null!));

		Parallel.For(0, BlockLength,
			i =>
			{
				if (i != 0)
				{
					var rd = BitConverter.GetBytes((ulong)i + Delta).ToBitArray();
					var concat = iv.Concat(rd);
					var temp = SymmetricEncryptor.Encrypt(concat.ToBytes()).ToBitArray();
					result[i] =  blocks[i].Xor(temp).ToBytes();	
				}
				else if (i == 0)
					result[i] = SymmetricEncryptor
						.Encrypt(
							BitConverter // прикольно то, что сейчас я все понимаю, но после сна пониманию звезда
								.GetBytes(IV)
								.ToBitArray()
								.Concat(BitConverter.GetBytes(Delta).ToBitArray())
								.Concat(new BitArray(Hash))
								.ToBytes()
						);
			});

		return result.SelectMany(s => s).ToArray();
	}

	public override byte[] Decrypt(byte[] value) //TODO
	{
		var blocks = new SimpleExpander(value, BlockLength)
			.Select(s => new BitArray(s))
			.ToList();

		var result = new List<byte[]>();
			
		blocks.ForEach(_ => result.Add(null!));
			
		// self care
		// don't believes that bitarray makes right binary operator &
		var mask = new BitArray(64,false).Concat(new BitArray(64, true));

		var iv = new BitArray(SymmetricEncryptor.Decrypt(blocks[0].ToBytes()));
			
		// supposed that hash length is 128/ Iv + delta length also 128
		var hash = new BitArray(128,false).Concat(new BitArray(128, true)).ToBytes();
			
		if (Hash != hash)
			throw new AuthenticationException("Hash is different => communication has been compromised");

		Parallel.For(0, BlockLength,
			(i, state) =>
			{
				var concat = iv.Concat(BitConverter.GetBytes((ulong)i + (ulong)i * Delta).ToBitArray());
				var temp = SymmetricEncryptor.Encrypt(concat.ToBytes()).ToBitArray();
				result[i] = blocks[i].Xor(temp).ToBytes();
			});

		return result.SelectMany(s => s).ToArray();
	}
}