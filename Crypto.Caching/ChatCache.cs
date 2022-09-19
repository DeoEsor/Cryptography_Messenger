using Crypto.Domain.Models;
using Redis.OM;
using Redis.OM.Searching;

namespace Crypto.Caching;

public sealed class ChatCache : IDisposable
{
    private bool _isDisposed;
    private readonly Dictionary<Guid, string> _chatToHash = new();

    public ChatCache(string connectionString)
    {
        Provider = new RedisConnectionProvider(connectionString);
    }

    ~ChatCache() => Dispose(false);

    public RedisConnectionProvider? Provider { get; private set; }
    public IRedisCollection<Message>? GetMessages
        => Provider?.RedisCollection<Message>();

    public async Task<string> Add(Message message)
    {
        if (_chatToHash.ContainsKey(message.Id))
            return _chatToHash[message.Id];
        
        var hash = await GetMessages?.InsertAsync(message)! 
                   ?? throw new ApplicationException("Cannot reach local db");
        _chatToHash.Add(message.Id, hash);
        return hash;
    }

    public async Task<Message?> SearchById(Guid messageId) 
        => _chatToHash.ContainsKey(messageId) 
            ? await GetMessages?.FindByIdAsync(_chatToHash[messageId])! 
              ?? throw new ApplicationException("Cannot reach local db")
            : null;

    /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
    public void Dispose() 
        => Dispose(true);
    
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