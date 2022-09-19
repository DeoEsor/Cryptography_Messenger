// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming

using CryptographyLib.Interfaces;
namespace CryptographyLib.CipherModes;

public static partial class CipherMode
{
	public enum Mode : byte { ECB,  CBC, CFB, OFB, CTR, RD, RDH }

	/// <summary>
	/// Generating Cipher mode
	/// </summary>
	/// <param name="mode">Cipher mode</param>
	/// <param name="symmetricEncryptor">Implementation of symmetric cipher algorithm <seealso cref="ISymmetricEncryptor"/></param>
	/// <param name="values">Additional parameters</param>
	/// /// 
	/// ///
	/// <returns>Cipher mode order</returns>
	/// <exception cref="ArgumentException">Additional parameters not valid</exception>
	/// <exception cref="NotImplementedException">Required Cipher mode not implemented</exception>
	/// <exception cref="ArgumentOutOfRangeException">Required Cipher mode not existed /</exception>
	public static CipherModeBase CreateInstance(Mode mode, ISymmetricEncryptor symmetricEncryptor, params object[] values)
		=> mode switch
		{
			Mode.ECB => Ecb(symmetricEncryptor, values),
			Mode.CBC => Cbc(symmetricEncryptor, values),
			Mode.CFB => Cfb(symmetricEncryptor, values),
			Mode.OFB => Ofb(symmetricEncryptor, values),
			Mode.CTR => Ctr(symmetricEncryptor, values),
			Mode.RD => Rd(symmetricEncryptor, values),
			Mode.RDH => Rdh(symmetricEncryptor, values),
			_ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
		};
}