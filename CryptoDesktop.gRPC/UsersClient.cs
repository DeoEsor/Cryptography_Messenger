using CryptoServices;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;

namespace CryptoDesktop.gRPC;

public sealed class UsersClient : IDisposable
{
	private readonly ILogger<UsersClient> _logger;
	private readonly UsersService.UsersServiceClient _client;
	private readonly GrpcChannel _channel;
	
	public UsersClient(ILogger<UsersClient> logger = null)
	{
		_logger = logger;

		var httpHandler = new HttpClientHandler();

		httpHandler.ServerCertificateCustomValidationCallback =
			HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
		
		_channel  =
			GrpcChannel
				.ForAddress("https://localhost:7173", new GrpcChannelOptions { HttpHandler = httpHandler });

		_client = new UsersService.UsersServiceClient(_channel);
	}

	public async Task<List<User>> GetUsersAsync()
	{
		var reply = await _client.GetUsersAsync(new UserGettingRequest());
		return reply.Users.ToList();
	}

	public void Dispose()
	{
		_channel.Dispose();
	}
}
