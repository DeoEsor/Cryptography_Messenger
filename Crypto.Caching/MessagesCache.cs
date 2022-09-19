using Crypto.Domain.Models;
using Redis.OM;
using Redis.OM.Searching;
using StackExchange.Redis;

namespace Crypto.Caching;

public sealed class MessagesCache : IDisposable
{
    private bool _isDisposed = false;
    public MessagesCache(string connectionString)
    {
        Provider = new RedisConnectionProvider(connectionString);
    }

    ~MessagesCache() => Dispose(false);

    public RedisConnectionProvider? Provider { get; private set; }
    public IRedisCollection<Message> GetMessages
        => Provider.RedisCollection<Message>();

    public async Task<string> AddMessageToCache(Message message)
    {
        return await GetMessages.InsertAsync(message);
    }
    
    public async Task<Message?> SearchMessageInCache(string messageId)
    {
        return await GetMessages.FindByIdAsync(messageId);
    }

    public void Dispose() => Dispose(true);
    
    private void Dispose(bool isDispose)
    {
        if (!_isDisposed)
        {
            Provider?.Connection.Dispose();
        }

        Provider = null;

        _isDisposed = true;
    }
}