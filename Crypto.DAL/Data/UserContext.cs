using Crypto.Domain;
using Crypto.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Crypto.DAL.Data;

public sealed class UserContext : DbContext
{
	private string? ConnectionString { get; set; }
	
	public UserContext(DbContextOptions<UserContext> options)
		: base(options)
	{}
	
	public UserContext(DbContextOptions<UserContext> options, string? connectionString)
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

	public DbSet<User> Users => Set<User>();
}