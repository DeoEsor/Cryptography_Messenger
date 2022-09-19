using Crypto.Domain.Serializers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Crypto.Domain.Models;

[BsonSerializer(typeof(MessageSerializer))]
public class Message
{
	public Guid Id { get; set; }
	
	public byte[] MessageData { get; set; } = null!;
	
	public string FileName { get; set; } = string.Empty;
}