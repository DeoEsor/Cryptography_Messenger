using System.Runtime.Serialization;
using Crypto.Domain.Models;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Crypto.Domain.Serializers;

public class MessageSerializer : SerializerBase<Message>
{
	public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Message value)
	{
		context.Writer.WriteStartDocument();
		
		context.Writer.WriteName(nameof(value.Id));
		context.Writer.WriteString(value.Id.ToString());
		
		context.Writer.WriteName(nameof(value.MessageData));
		context.Writer.WriteBytes(value.MessageData);
		
		context.Writer.WriteName(nameof(value.FileName));
		context.Writer.WriteString(value.FileName);
		
		context.Writer.WriteEndDocument();
	}

	public override Message Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
	{
		var res = new Message();
		context.Reader.ReadStartDocument();

		if (context.Reader.ReadString() != nameof(res.Id))
			throw new AggregateException(new SerializationException("Deserialization failed with incorrect file"));
		
		res.Id = Guid.Parse(context.Reader.ReadString());
		
		if (context.Reader.ReadString() != nameof(res.MessageData))
			throw new AggregateException(new SerializationException("Deserialization failed with incorrect file"));

		res.MessageData = context.Reader.ReadBytes();
		
		if (context.Reader.ReadString() != nameof(res.FileName))
			throw new AggregateException(new SerializationException("Deserialization failed with incorrect file"));
		
		res.FileName = context.Reader.ReadString();
		
		context.Reader.ReadEndDocument();
		return res;
	}
}