using Crypto.Domain.Models;
using Redis.OM;
using Redis.OM.Searching;

namespace Crypto.Caching;

public sealed class UsersCache : IDisposable
{
    private Dictionary<string, string> userDictionary = new Dictionary<string, string>();
    
    private bool _isDisposed;
    
    public UsersCache(string connectionString)
    {
        Provider = new RedisConnectionProvider(connectionString);
    }

    ~UsersCache() => Dispose(false);

    public RedisConnectionProvider? Provider { get; private set; }
    public IRedisCollection<User> GetMessages
        => Provider.RedisCollection<User>();

    public async Task<string> AddUserToCache(User message)
    {
        return await GetMessages.InsertAsync(message);
    }
    
    public async Task<User?> SearchUserInCache(string userId)
    {
        return await GetMessages.FindByIdAsync(userId);
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