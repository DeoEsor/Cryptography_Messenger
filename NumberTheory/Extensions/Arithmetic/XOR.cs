// ReSharper disable InconsistentNaming
namespace NumberTheory.Extensions.Arithmetic;

public static partial class ArithmeticExtensions
{
	public static int XOR(int a, int b) => a ^ b;
		
	public static int XOR(ushort a, ushort b) => a ^ b;
		
	public static byte[] XOR(byte[] a, byte[] b)
	{
		var greater = a.Length > b.Length ? a.Clone() as byte[] : b.Clone() as byte[];
		var less = a.Length > b.Length ? b : a;
			
		if (greater == null) return null!;

		for (var i = 0; i < less.Length; i++)
			greater[i] ^= less[i];

		return greater!;
	}
}