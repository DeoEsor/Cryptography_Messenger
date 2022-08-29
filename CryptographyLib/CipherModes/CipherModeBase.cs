using CryptographyLib.Interfaces;
namespace CryptographyLib.CipherModes
{
	public abstract class CipherModeBase : IEncryptor, IDecryptor
	{
		private IEncryptor _encryptor = null!;
		private IDecryptor _decryptor = null!;
		protected readonly ISymmetricEncryptor SymmetricEncryptor;
		
		public int BlockLength { get; set; }
		public long Iv { get; set; }

		protected IEncryptor Encryptor
		{
			get => _encryptor ?? SymmetricEncryptor;
			set => _encryptor = value;
		}

		protected IDecryptor Decryptor
		{
			get => _decryptor ?? SymmetricEncryptor;
			set => _decryptor = value;
		}

		protected CipherModeBase(ISymmetricEncryptor symmetricEncryptor, int blockLength = 8)
		{
			SymmetricEncryptor = symmetricEncryptor;
			BlockLength = blockLength;
		}
		
		public abstract byte[] Encrypt(byte[] value);
		public abstract byte[] Decrypt(byte[] value);
	}
}
