using PhoneApi.DBRepository.Interfaces;
using PhoneApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.DBRepository.Repositories
{
    public class PhoneRepository : BaseRepository, IPhoneRepository
    {
        public PhoneRepository(string connectionString, IRepositoryContextFactory contextFactory) : base(connectionString, contextFactory) { }
        public async Task AddPhone(Phone phone)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var result =
                context.Phones.Add(phone);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Phone>> GetAllPhones()
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return context.Phones.ToList();
            }
        }

        public async Task<Phone> GetPhone(int phoneId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Phones.FirstOrDefaultAsync(p => p.Id == phoneId);
            }
        }
    }
}
