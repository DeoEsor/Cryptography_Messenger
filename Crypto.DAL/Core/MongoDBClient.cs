using MongoDB.Driver;

namespace Crypto.DAL.Core;

public class MongoDbClient
{
    private string ConnectionString { get; set; }
    private MongoClient Client { get; set; }
	
    public MongoDbClient(string connectionString)
    {
        ConnectionString = connectionString;
        var settings = MongoClientSettings.FromConnectionString(ConnectionString);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        Client = new MongoClient(settings);
    }

    public IMongoDatabase GetDatabase(string database) => Client.GetDatabase(database);
}