using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Repository;

namespace CompanyEmployee.ContextFactory
{
    public class RepositoryContextFactory:IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<RepositoryContext>()
                .UseNpgsql(configuration.GetConnectionString("posgresConnectionString"),
                b=>b.MigrationsAssembly("CompanyEmployee"));
            return new RepositoryContext(builder.Options);
        }
    }
}
