using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace CryptographyLib.Extensions
{
	public static class ByteExtensions
	{
		public static BitArray ToBitArray(this byte[] value) => new BitArray(value);
		/// <summary>
		/// Get k's bit from <paramref name="value"/>
		/// </summary>
		/// <param name="value">byte(bits) array</param>
		/// <param name="k">position of bit in <paramref name="value"/></param>
		/// <returns>k's bit</returns>
		public static byte GetKBit(this int value, int k)
			=> (byte)((value >> k) & 1);
		
		/// <inheritdoc cref="GetKBit(int,int)"/>>
		public static byte GetKBit(this byte[] value, int k)
			=> (byte)((value[k / 8] >> k % 8) & 1);
		
		/// <summary>
		/// Separates array of bytes into byte blocks
		/// </summary>
		/// <param name="value">array of bytes</param>
		/// <param name="blockLength">Length of block (1,2,4,8,16 and etc.)</param>
		/// <returns>Blocks of bytes with <paramref name="blockLength"/> lenght</returns>
		public static IEnumerable<byte[]> GetByteBlocks(this byte[] value, [Range(1,8)] int blockLength = 1)
		{
			int current = 0;
			List<byte> list = new List<byte>(blockLength);
			while (current < value.Length)
			{
				for (int i = 0; i < blockLength; i++)
					list.Add(value[current++]);
				
				yield return list.ToArray();
				list.Clear();
			}
		}
		
		/// <summary>
		/// Get k's bit from <paramref name="value"/>
		/// </summary>
		/// <param name="value">byte(bits) array</param>
		/// <param name="k">position of bit in <paramref name="value"/></param>
		/// <returns>k's bit</returns>
		public static byte GetKByte(this int value, int k)
			=> (byte)((value >> (k * 4)) & 0b1111);

		/// <summary>
		/// Count of bites in array of bytes
		/// </summary>
		/// <param name="_value">array of bytes</param>
		/// <returns>Count of bites</returns>
		public static int CountOfBites(this int value) 
			=> Math.ILogB(value.MaxValuableBit()) + 1;

		/// <summary>
		/// Returns max valuable bit in array of bytes
		/// </summary>
		/// <param name="value">array of bytes</param>
		/// <returns>value of bit</returns>
		public static int MaxValuableBit(this int value)
		{
			var val = value;
			val |= val >>  1;
			val |= val >>  2;
			val |= val >>  4;
			val |= val >>  8;
			val |= val >> 16;
			return val - (val >> 1);
		}
		
		/// <summary>
		/// Returns collection of bits in <paramref name="value"/>
		/// </summary>
		/// <param name="value">array of bytes</param>
		/// <returns>collection of bits</returns>
		public static IEnumerable<byte> GetBits(this int value)
		{
			int z =value.CountOfBites();
			
			while (z-- > 0) 
				yield return value.GetKBit(z);
		}
		
		/// <summary>
		/// Returns collection of bits in <paramref name="value"/>
		/// </summary>
		/// <param name="value">array of bytes</param>
		/// <returns>collection of bits</returns>
		public static IEnumerable<byte> GetBytes(this int value)
		{
			int z = value.CountOfBites() / 4;
			if (z == 0) z = 1;
			while (z-- > 0)
				yield return value.GetKByte(z);
		}
		
		public static byte[] XorBytes(this long a, long b)
			=> BitConverter.GetBytes(a ^ b);
		public static byte[] XorBytes(this byte[] a, byte[] b)
			=> BitConverter.GetBytes(BitConverter.ToInt64(a) ^ BitConverter.ToInt64(b));
		public static byte[] XorBytes(this long a, byte[] b)
			=> BitConverter.GetBytes(a ^ BitConverter.ToInt64(b));
		public static byte[] XorBytes(this int a, long b)
			=> BitConverter.GetBytes(a ^ b);
		public static byte[] XorBytes(this int a, byte[] b)
			=> BitConverter.GetBytes(a ^ BitConverter.ToInt64(b));


		public static object UniteBlock(byte[] value, out Type type)
		{
			switch (value.Length)
			{
				case 1:
					type = typeof(byte);
					return value[0];
				case 2:
					type = typeof(ushort);
					return UnityByteToUShort(value);
				case 4:
					type = typeof(int);
					return UnityByteToInt(value);
				case 8:
					type = typeof(long);
					return UnityByteToLong(value);
				default:
					throw new ArgumentException();
			}
		}

		#region Private Methods
		private static long UnityByteToLong(byte[] value)
		{
			long result = Int64.MinValue;

			foreach (var variable in value)
			{
				result <<= 4;
				result |= variable;
			}

			return result;
		}
		
		private static int UnityByteToInt(byte[] value)
		{
			int result = Int32.MinValue;

			foreach (var variable in value)
			{
				result <<= 4;
				result |= variable;
			}

			return result;
		}
		
		private static ushort UnityByteToUShort(byte[] value)
		{
			ushort result = UInt16.MaxValue;

			foreach (var variable in value)
			{
				result <<= 4;
				result |= variable;
			}

			return result;
		}
  #endregion
	}
}
