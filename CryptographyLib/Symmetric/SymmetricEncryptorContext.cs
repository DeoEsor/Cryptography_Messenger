using System.Collections.Concurrent;
using CryptographyLib.CipherModes;
using CryptographyLib.Interfaces;

// ReSharper disable InconsistentNaming
namespace CryptographyLib.Symmetric;

public sealed class SymmetricEncryptorContext
{
	private ushort Seed;

	private CipherModeBase Mode;
		
	public SymmetricEncryptorContext(CipherMode.Mode mode, 
		ushort seed, 
		ISymmetricEncryptor symmetricEncryptor, params object[] parametrs)
	{
		Mode = CipherMode.CreateInstance(mode, symmetricEncryptor, parametrs);
		Seed = seed;
	}

	public byte[] Encrypt(byte[] value) =>  Mode.Encrypt(value);
	public byte[] Decrypt(byte[] value) =>  Mode.Decrypt(value);
	public async Task<byte[]> EncryptAsync(byte[] value) =>  Mode.Encrypt(value);
	public async Task<byte[]> DecryptAsync(byte[] value) =>  Mode.Decrypt(value);
		
	public async Task AsyncEncryptFile(string pathFileInput, string  pathFileOutput)
	{
		if (File.Exists(pathFileOutput)) File.Delete(pathFileOutput);

		var input = new ConcurrentQueue<byte>();
			
		using (var reader = new BinaryReader(File.Open(pathFileInput, FileMode.Open)))
			while (reader.PeekChar() > -1)
				input.Enqueue(reader.ReadByte());

		await using var writer = new BinaryWriter(File.Create(pathFileOutput));
		writer.Write(Mode.Encrypt(input.ToArray()));
	}
		
	public async Task AsyncDecryptFile(string pathFileInput, string  pathFileOutput)
	{
		if (File.Exists(pathFileOutput)) 
			File.Delete(pathFileOutput);

		var input = new ConcurrentQueue<byte>();
		using (var reader = new BinaryReader(File.Open(pathFileInput, FileMode.Open)))
			while (reader.PeekChar() > -1)
				input.Enqueue(reader.ReadByte());

		await using (var writer = new BinaryWriter(File.Create(pathFileOutput)))
			writer.Write(Mode.Decrypt(input.ToArray()));
	}
}