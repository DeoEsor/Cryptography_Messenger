using CryptoServices;
using Google.Protobuf;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
namespace CryptoDesktop.gRPC;

public sealed class AuthClient : IDisposable
{
	private readonly ILogger<AuthClient> _logger;
	private readonly AuthService.AuthServiceClient _client;
	private readonly GrpcChannel _channel;
	
	public AuthClient(ILogger<AuthClient> logger = null)
	{
		_logger = logger;

		var httpHandler = new HttpClientHandler();

		httpHandler.ServerCertificateCustomValidationCallback =
			HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
		
		_channel  =
			GrpcChannel
				.ForAddress("https://localhost:7173", new GrpcChannelOptions { HttpHandler = httpHandler });

		_client = new AuthService.AuthServiceClient(_channel);
	}
	
	public async Task<StartSessionReply> AuthAsync(string Username, string Password, CancellationToken token = default)
	{
		var request = new StartSessionRequest
		{
			Username = Username,
			Password = ByteString.CopyFromUtf8(Password)
		};
		return await _client.AuthAsync(request,null,null,token);
	}
	
	public async Task<StartSessionReply> Register(string Username, string Password)
	{
		var request = new StartSessionRequest
		{
			Username = Username,
			Password = ByteString.CopyFromUtf8(Password)
		};
		return await _client.RegisterAsync(request);
	}
	public async Task Quit(string Username, string Password)
	{
		await _client.QuitAsync(new FinishSessionRequest
		{
			Username = Username,
			Password = ByteString.CopyFromUtf8(Password)
		});
	}

	public void Dispose()
	{
		_channel.Dispose();
	}
}
