using Crypto.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Crypto.DAL.Factories;

public class UserContextFactory : IDesignTimeDbContextFactory<UserContext>
{
    public UserContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UserContext>();
        
        optionsBuilder
            .UseSqlServer("Data Source=localhost,1433;Database=CryptoDB;Trusted_Connection=False;User ID=sa;Password=Pa55w0rd");

        return new UserContext(optionsBuilder.Options);
    }
}