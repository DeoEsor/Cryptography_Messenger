// ReSharper disable CheckNamespace

using CryptographyLib.Paddings;
namespace CryptographyLib;

public static class Padding
{
	public enum PaddingMode {PKCS7, ISO_10126 ,X923}

	public static IPadding CreateInstance(PaddingMode mode)
	{
		return mode switch
		{
			PaddingMode.PKCS7 => new PKCS7(),
			PaddingMode.ISO_10126 => new ISO_10126(),
			PaddingMode.X923 => new X923(),
			_ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
		};
	}
}