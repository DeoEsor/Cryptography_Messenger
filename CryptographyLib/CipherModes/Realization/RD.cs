using System.Collections;
using CryptographyLib.Expanders;
using CryptographyLib.Extensions.BitManipulationsExtensions;
using CryptographyLib.Interfaces;
using CryptographyLib.KeyExpanders;
using NUnit.Framework;

namespace CryptographyLib.CipherModes.Realization;

public class RD : CipherModeBase
{
	public ulong Delta { get; set; }
	public RD(ISymmetricEncryptor symmetricEncryptor, long iv, int BlockLength = 8, ulong rd = UInt64.MinValue) 
		: base(symmetricEncryptor, BlockLength)
	{
		IV = iv;
		Delta = rd == UInt64.MinValue ? TestContext.CurrentContext.Random.NextULong() : rd;
		/*
		 * if (Delta % 2 == 0)
		 *		Delta += 1;
		 */
	}

	public override byte[] Encrypt(byte[] value)
	{
		var iv = new BitArray(BitConverter.GetBytes(IV));

		var blocks = new SimpleExpander(value, BlockLength)
			.Select(s => new BitArray(s))
			.ToList();

		var result = new List<byte[]>();
			
		blocks.ForEach(_ => result.Add(null!));
		result.Add(null!);

		Parallel.For(0, BlockLength + 1,
			(i, state) =>
			{
				if (i != 0)
				{
					var rd = BitConverter.GetBytes((ulong)i + Delta).ToBitArray();
					var concat = iv.Concat(rd);
					var temp = SymmetricEncryptor.Encrypt(concat.ToBytes()).ToBitArray();
					result[i] =  blocks[i].Xor(temp).ToBytes();	
				}
				else
					result[i] = SymmetricEncryptor
						.Encrypt(
							BitConverter // прикольно то, что сейчас я все понимаю, но после сна пониманию звезда
								.GetBytes(IV)
								.ToBitArray()
								.Concat(BitConverter.GetBytes(Delta).ToBitArray())
								.ToBytes()
						);
			}); 
		// что-то здесь не так ссылаясь на https://drive.google.com/file/d/1cgiwXPfFK6dy1z4v6Vu2-7otA4pJu3Io/view
		// после дешифровки IV я должен буду получать Delta. но
		// 1. В разных источниках говорится, что я лишь прикрепляю каунтер к IV, но это ок, если следовать схеме - все ок
		// 2. допустим IV - 128 бит и я меняю лишь последние 64 как из изначального IV я получу delta не делая никаких преобразований - вопрос

		return result.SelectMany(s => s).ToArray();
	}

	public override byte[] Decrypt(byte[] value)
	{
		var blocks = new SimpleExpander(value, BlockLength)
			.Select(s => new BitArray(s))
			.ToList();

		var result = new List<byte[]>();
			
		blocks.ForEach(_ => result.Add(null!));
		result.RemoveAt(blocks.Count);

			
		// self care
		// don't believes that bitarray makes right binary operator &
		var mask = new BitArray(64,false).Concat(new BitArray(64, true));

		var iv = new BitArray(SymmetricEncryptor.Decrypt(blocks[0].ToBytes()));
		Delta = BitConverter.ToUInt64(iv.And(mask).ToBytes());
			

		Parallel.For(0, BlockLength + 1,
			i =>
			{
				var concat = iv.Concat(BitConverter.GetBytes((ulong)i + (ulong)i * Delta).ToBitArray());
				var temp = SymmetricEncryptor.Encrypt(concat.ToBytes()).ToBitArray();
				result[i] = blocks[i].Xor(temp).ToBytes();
			});

		return result.SelectMany(s => s).ToArray();
	}
}