﻿using System.Dynamic;
using System.Numerics;

namespace CryptographyLib.Data;

public sealed class Key
{
	public enum KeyTypeEnum
	{
		Symmetric,
		Asymmetric
	}
	
	public static Key CreateSymmetricKey(byte[] symmetricKey) 
		=> new(symmetricKey);
	
	public static Key CreateAsymmetricKey(byte[] publicKey, byte[] privateKey)
		=> new(publicKey, privateKey);

	/// <summary>
	/// format of serialization			-> [first 4 bytes - length of array]
	/// <br/>
	/// continues with foreach value	-> [first 4 bytes - count of bytes that contains value, array of bytes that representating BigInt] 
	/// </summary>
	/// <param name="publicKey"></param>
	/// <param name="privateKey"></param>
	/// <returns></returns>
	public static Key CreateAsymmetricKey(BigInteger[] publicKey, BigInteger[] privateKey)
	{
		var publicBytes = new List<byte>(); 
		var privateBytes = new List<byte>(BitConverter.GetBytes(privateKey.Length));
		publicBytes.AddRange(BitConverter.GetBytes(publicKey.Length));
		privateBytes.AddRange(BitConverter.GetBytes(privateKey.Length));

		foreach (var value in publicKey)
		{
			var bytes = value.ToByteArray();
			publicBytes.AddRange(BitConverter.GetBytes(bytes.Length));
			publicBytes.AddRange(bytes);
		}
		
		foreach (var value in privateKey)
		{
			var bytes = value.ToByteArray();
			privateBytes.AddRange(BitConverter.GetBytes(bytes.Length));
			privateBytes.AddRange(bytes);
		}
		
		
		return new Key(publicBytes.ToArray(), privateBytes.ToArray());
	}
	
	private Key(byte[] symmetricKey)
	{
		KeyType = KeyTypeEnum.Symmetric;
		SymmetricKey = symmetricKey;
	}
	
	private Key(byte[] publicKey, byte[] privateKey)
	{
		KeyType = KeyTypeEnum.Asymmetric;
		PublicKey = publicKey;
		PrivateKey = privateKey;
	}

	public byte[] SymmetricKey { get; } = null!;

	public byte[] PublicKey { get; } = null!;

	public byte[] PrivateKey { get; } = null!;

	public KeyTypeEnum KeyType { get; }

	public static BigInteger[] GetBigInts(byte[] bytes)
	{
		var res = new BigInteger[BitConverter.ToInt32(bytes.AsSpan(0, 4))];
		var startIndex = 4;

		for (var i = 0; i < res.Length; i++)
		{
			var length = BitConverter.ToInt32(bytes.AsSpan(startIndex, 4));
			var value = new BigInteger(bytes.AsSpan(startIndex + 4, length));
			startIndex += 4 + length;
			res[i] = value;
		}
		
		return res;
	}

	public static byte[] SerializeBigInts(BigInteger[] array)
	{
		var res = new List<byte>(); 
		res.AddRange(BitConverter.GetBytes(array.Length));

		foreach (var value in array)
		{
			var bytes = value.ToByteArray();
			res.AddRange(BitConverter.GetBytes(bytes.Length));
			res.AddRange(bytes);
		}

		return res.ToArray();
	}
	
}