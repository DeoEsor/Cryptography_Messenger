// ReSharper disable CheckNamespace

using CryptographyLib.Interfaces;
namespace CryptographyLib.Paddings;

/// <summary>
/// Supplier for Paddings realizations
/// </summary>
public static partial class Padding
{
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