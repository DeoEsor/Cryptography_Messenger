using Crypto.DAL.Core;
using Crypto.Domain.Models;
using Crypto.Domain.Serializers;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Crypto.DAL.Data;

public class MessageContext
{
	private readonly string _collectionName;
	public MongoDbClient? Client { get; }
	private IMongoDatabase? Database { get; set; }

	static MessageContext()
	{
		BsonSerializer.RegisterSerializer(new MessageSerializer());	
	}
	
	public MessageContext(MongoDbClient client, string databaseName, string collectionName)
	{
		_collectionName = collectionName;
		Client = client;
		Database = Client?.GetDatabase(databaseName);
	}

	public IMongoCollection<Message>? GetMessages 
		=> Database?.GetCollection<Message>(_collectionName);

	public async Task<Message>? FindMessageById(Guid messageId)
	{
		var filter = Builders<Message>.Filter.Eq("Id", messageId.ToString());

		return await (await GetMessages.FindAsync(filter))
			.FirstOrDefaultAsync();
	}

	public async Task AddMessage(Message message)
	{
		await GetMessages?.InsertOneAsync(message)!;
	}
}