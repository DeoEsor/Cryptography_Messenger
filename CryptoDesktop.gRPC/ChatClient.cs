using CryptoServices;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;

namespace CryptoDesktop.gRPC;

public sealed class ChatClient : IDisposable
{
	private readonly ILogger<ChatClient> _logger;
	private readonly ChattingService.ChattingServiceClient _client;
	private readonly GrpcChannel _channel;
	
	public ChatClient(ILogger<ChatClient> logger = null)
	{
		_logger = logger;

		var httpHandler = new HttpClientHandler();

		httpHandler.ServerCertificateCustomValidationCallback =
			HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
		
		_channel  =
			GrpcChannel
				.ForAddress("https://localhost:7173", new GrpcChannelOptions { HttpHandler = httpHandler });

		_client = new ChattingService.ChattingServiceClient(_channel);
	}

	public AsyncUnaryCall<StartChattingReply> StartChattingAsync(ulong id)
	{
		var request = new StartChattingRequest()
		{
			TargetId = id
		};
		return _client.StartChattingAsync(request);
	}

	public async Task SendMessage(
		Grpc.Core.IAsyncStreamReader<OpenMessage> requestStream,
		Grpc.Core.IServerStreamWriter<OpenMessage> responseStream,
		Grpc.Core.ServerCallContext context)
	{
		while (await requestStream.MoveNext())
		{
			var note = requestStream.Current;
			/*List<OpenMessage> prevNotes = AddNoteForLocation(note.Location, note);
			foreach (var prevNote in prevNotes)
			{
				await responseStream.WriteAsync(prevNote);
			}
			*/
		}
	}

	public void Dispose()
	{
		_channel.Dispose();
	}
}