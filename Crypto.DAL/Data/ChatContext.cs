using Crypto.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Crypto.DAL.Data;

public class ChatContext : DbContext
{
	private string? ConnectionString { get; set; }
	
	public ChatContext(DbContextOptions<ChatContext> options)
		: base(options)
	{ }
	
	public ChatContext(DbContextOptions<ChatContext> options, string? connectionString)
		: base(options)
	{
		ConnectionString = connectionString;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
		optionsBuilder
			.UseSqlServer(ConnectionString);
	}

	public DbSet<Chat> Chats => Set<Chat>();
}