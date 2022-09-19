//using CryptoServer.Services;

using CryptoServer.Core;
using CryptoServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<Repository>();
builder.Services.AddGrpc();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ChattingService>();
app.MapGrpcService<AuthService>();
app.MapGrpcService<UsersService>();
app.Run();
