using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Quizzler.Dal.Data.DbContextData
{
    internal class MainDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
    {

        public MainDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MainDbContext>();

            var connectionString = @"Server=.\SQLEXPRESS; Database=QuizPro2; TrustServerCertificate=True; Trusted_Connection=true; Integrated Security=true";

            optionsBuilder.UseSqlServer(connectionString);
            return new MainDbContext(optionsBuilder.Options);
        }
    }
}
