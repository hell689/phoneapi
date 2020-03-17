using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PhoneApi.DBRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi
{
    public class DesignTimeRepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
	{
		public RepositoryContext CreateDbContext(string[] args)
		{
			var builder = new ConfigurationBuilder()
					  .SetBasePath(Directory.GetCurrentDirectory())
				  .AddJsonFile("appsettings.json");

			var config = builder.Build();
			var connectionString = config.GetConnectionString("DefaultConnection");
			var repositoryFactory = new RepositoryContextFactory();

			return repositoryFactory.CreateDbContext(connectionString);
		}
	}
}
