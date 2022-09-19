using NumberTheory;

namespace CryptographyLib.Symmetric;

public sealed partial class Magenta
{
	#region Magenta Functions

	private static byte F(byte x)
		=> x == 255
			? (byte)0
			: GaloisField.PowerGf256(0x02, x, 0x65);

	private static byte A(byte x, byte y) => F((byte)(x ^ F(y)));

	private static byte[] Pe(byte x, byte y)
	{
		var result = new byte[2];
		result[0] = A(x, y);
		result[1] = A(y, x);

		return result;
	}

	private static byte[] P(byte[] x)
	{
		var res16 = new byte[16];

		for (var i = 0; i < 8; i++)
			res16[i] = Pe(x[i], x[8 + i])[i % 2];

		return res16;
	}

	private static byte[] T(byte[] x) => P(P(P(P(x))));

	private static byte[] Xe(byte[] x)
	{
		var result = new byte[8];

		for (var i = 0; i < 8; i++)
			result[i] = x[2 * i];

		return result;
	}

	private static byte[] Xo(byte[] x)
	{
		var result = new byte[8];

		for (var i = 0; i < 8; i++)
			result[i] = x[2 * i + 1];

		return result;
	}

	private static byte[] C1(byte[] x) => T(x);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="x"></param>
	/// <returns></returns>
	private static byte[] C2(byte[] x)
	{
		var y = new byte[16];

		for (var i = 0; i < 8; i++)
			y[i] = (byte)(x[i] ^ Xe(C1(x))[i]);

		for (var i = 8; i < 16; i++)
			y[i] = (byte)(x[i] ^ Xo(C1(x))[i - 8]);

		return T(y);
	}

	private static byte[] C3(byte[] x)
	{
		var y = new byte[16];

		for (var i = 0; i < 8; i++)
			y[i] = (byte)(x[i] ^ Xe(C2(x))[i]);

		for (var i = 8; i < 16; i++)
			y[i] = (byte)(x[i] ^ Xo(C2(x))[i - 8]);

		return T(y);
	}

	private static byte[] E(byte[] x) => Xe(C3(x));

	private static byte[,] F(IReadOnlyList<byte> xLeft, IReadOnlyList<byte> xRight, IReadOnlyList<byte> y8)
	{
		var z = new byte[2, 8];
		var xy = new byte[16];

		for (var i = 0; i < 8; i++)
			(xy[i], xy[i + 8]) = (xRight[i], y8[i]);

		for (var i = 0; i < 8; i++)
			(z[0, i], z[1, i]) = (xRight[i], (byte)(xLeft[i] ^ E(xy)[i]));

		return z;
	}

	#endregion
}