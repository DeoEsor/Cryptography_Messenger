using CryptoServer.Core;
using CryptoServices;
using Grpc.Core;

namespace CryptoServer.Services
{
	public class ChattingService : CryptoServices.ChattingService.ChattingServiceBase
	{
		private readonly ILogger<ChattingService> _logger;

		private readonly Repository Db;

		public ChattingService(ILogger<ChattingService> logger, Repository db)
		{
			_logger = logger;
			Db = db;
		}

		public override Task<StartChattingReply> StartChatting(StartChattingRequest request, ServerCallContext context)
		{
			return base.StartChatting(request, context);
		}

		public override Task SendMessage(IAsyncStreamReader<OpenMessage> requestStream, IServerStreamWriter<OpenMessage> responseStream, ServerCallContext context)
		{
			return base.SendMessage(requestStream, responseStream, context);
		}
	}
}
