using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.DBRepository
{
    public class RepositoryContextFactory : IRepositoryContextFactory
    {
        public RepositoryContext CreateDbContext(string connectionString)
        {
            var optionBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionBuilder.UseSqlite(connectionString);

            return new RepositoryContext(optionBuilder.Options);
        }
    }
}
