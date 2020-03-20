using PhoneApi.DBRepository.Interfaces;
using PhoneApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.DBRepository.Repositories
{
    public class CabinetRepository : BaseRepository, ICabinetRepository
    {
        public CabinetRepository(string connectionString, IRepositoryContextFactory contextFactory) : base(connectionString, contextFactory) { }
        public async Task AddCabinet(Cabinet cabinet)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                //var result =
                context.Cabinets.Add(cabinet);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Cabinet>> GetAllCabinets()
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return context.Cabinets.ToList();
            }
        }

        public async Task<Cabinet> GetCabinet(int cabinetId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return context.Cabinets.FirstOrDefault(c => c.Id == cabinetId);
            }
        }

        public async Task DeleteCabinet(int cabinetId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var cabinet = new Cabinet() { Id = cabinetId };
                context.Cabinets.Remove(cabinet);
                await context.SaveChangesAsync();
            }
        }
    }
}
