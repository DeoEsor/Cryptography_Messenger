using CryptoServer.Interfaces;
using CryptoServices;
using Grpc.Core;

namespace CryptoServer.Services;

public sealed class AuthService : CryptoServices.AuthService.AuthServiceBase
{
    private readonly ILogger<AuthService> _logger;

    private readonly Repository _db;

    public AuthService(ILogger<AuthService> logger, Repository db)
    {
        _logger = logger;
        _db = db;
    }
    
    public override async Task<StartSessionReply> Register(StartSessionRequest request, ServerCallContext context)
    {
        var reply = new StartSessionReply
        {
            StatusCode = (int)StatusCode.OK
        };
        try
        {
            var random = new Random();
            _logger.LogInformation($"Getted to register {request.Username}");
            if (((IRepository<User>)_db).GetDataList().FirstOrDefault(s => s.Username == request.Username) != null)
            {
                reply.StatusCode = (int)StatusCode.AlreadyExists;
                reply.Comment = "Login reserved";
                _logger.LogInformation("Same user already registrated");
                return reply;
            }
            
            _db?.AddData(new Core.User
            {
                Username   = request.Username,
                Color = $"#{random.Next(0x1000000):X6}"
            });
            var user = ((IRepository<User>)_db)?.GetDataList()
                .FirstOrDefault(user => user.Username == request.Username);
            if (user == null)
            {
                reply.StatusCode = (int)StatusCode.Aborted;
                reply.Comment = "Wrong login or password";
                return reply;
            }

            reply.User = new User
            {
                Id = user.Id,
                Username = user.Username,
                Color = user.Color,
                ImageSource = user.ImageSource ?? ""
            };
            return reply;
        }
        catch(Exception e)
        {
            _logger.LogCritical("{EMessage}", e.Message);
            reply.StatusCode = (int)StatusCode.Aborted;
            return reply;
        }
    }

    public override Task<StartSessionReply> Auth(StartSessionRequest request, ServerCallContext context)
    {
        var reply = new StartSessionReply
        {
            StatusCode = (int)StatusCode.OK
        };
        var user = _db.ChangeStatus(request.Username, request.Password.ToStringUtf8(), true);
        if (user == null)
        {
            reply.StatusCode = (int)StatusCode.NotFound;
            reply.Comment = "Wrong login or password";
            return Task.FromResult(reply);
        }

        reply.User = new User
        {
            Id = (ulong)user.Id,
            Username = user.Username,
            Color = user.Color,
            ImageSource = user.ImageSource ?? ""
        };
        return Task.FromResult(reply);
    }

    public override async Task<FinishSessionReply> Quit(FinishSessionRequest request, ServerCallContext context)
    {
        var user = _db.ChangeStatus(request.Username,request.Password.ToStringUtf8(), false);
        return new FinishSessionReply
        {
            Id = (ulong)user.Id
        };
    }
}