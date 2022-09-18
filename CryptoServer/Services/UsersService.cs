using CryptoServer.Core;
using CryptoServices;
using Grpc.Core;

namespace CryptoServer.Services;

public class UsersService : CryptoServices.UsersService.UsersServiceBase
{
    private readonly ILogger<UsersService> _logger;

    private readonly Repository _db;

    public UsersService(ILogger<UsersService> logger, Repository db)
    {
        _logger = logger;
        _db = db;
    }

    public override Task<UserGettingReply> GetUsers(UserGettingRequest request, ServerCallContext context)
    {
        return base.GetUsers(request, context);
    }
}