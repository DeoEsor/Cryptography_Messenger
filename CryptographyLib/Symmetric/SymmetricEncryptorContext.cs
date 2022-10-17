using System.Collections.Concurrent;
using CryptographyLib.CipherModes;
using CryptographyLib.Interfaces;
using CryptographyLib.Paddings;

// ReSharper disable InconsistentNaming
namespace CryptographyLib.Symmetric;

public sealed class SymmetricEncryptorContext
{
	private ushort Seed;

	private CipherModeBase Mode;
		
	public SymmetricEncryptorContext(CipherMode.Mode mode, 
		ushort seed, 
		ISymmetricEncryptor symmetricEncryptor, params object[] parameters)
	{
		Mode = CipherMode.CreateInstance(mode, symmetricEncryptor, parameters);
		Seed = seed;
	}

	public byte[] Encrypt(byte[] value) =>  Mode.Encrypt(value);
	public byte[] Decrypt(byte[] value) =>  Mode.Decrypt(value);
	public Task<byte[]> EncryptAsync(byte[] value) =>  Task.FromResult(Mode.Encrypt(value));
	public Task<byte[]> DecryptAsync(byte[] value) =>  Task.FromResult(Mode.Decrypt(value));
		
	public async Task AsyncEncryptFile(string pathFileInput, string  pathFileOutput)
	{
		if (File.Exists(pathFileOutput)) 
			File.Delete(pathFileOutput);

		byte[] value;

		await using var writer = new BinaryWriter(File.Create(pathFileOutput));
		using var reader = new BinaryReader(File.Open(pathFileInput, FileMode.Open));
		while ((value = reader.ReadBytes(128)).Length != 0)
			writer.Write(await EncryptAsync(value));
	}
		
	public async Task AsyncDecryptFile(string pathFileInput, string pathFileOutput)
	{
		byte[] value;
		if (File.Exists(pathFileOutput)) 
			File.Delete(pathFileOutput);

		await using var writer = new BinaryWriter(File.Create(pathFileOutput));
		using var reader = new BinaryReader(File.Open(pathFileInput, FileMode.Open));
		while ((value = reader.ReadBytes(128)).Length != 0)
		{
			if (value.Length != 128)
			{
				value = new PKCS7().DeletePadding(await DecryptAsync(value));
				writer.Write(value);
				break;
			}
			writer.Write(await DecryptAsync(value));
		}
	}

	private async Task WriteBlock(StreamReader reader, StreamWriter writer, long from, long to)
	{
		/*
		char[] value = new char[128];
		reader.ReadAsync(value, from, 128);
		writer.Write(await DecryptAsync(value.));
		*/
	}
}