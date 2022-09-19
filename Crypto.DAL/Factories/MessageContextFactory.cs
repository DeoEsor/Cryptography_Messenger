using Crypto.DAL.Data;
using Crypto.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Crypto.DAL.Factories;

public class ChatContextFactory : IDesignTimeDbContextFactory<ChatContext>
{
    public ChatContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ChatContext>();
        
        optionsBuilder
            .UseSqlServer("Data Source=localhost,1433;Database=CryptoDB;Trusted_Connection=False;User ID=sa;Password=Pa55w0rd");

        return new ChatContext(optionsBuilder.Options);
    }
}