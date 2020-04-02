using PhoneApi.DBRepository.Interfaces;
using PhoneApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.DBRepository.Repositories
{
    public class CabinetPhoneRepository : BaseRepository, ICabinetPhoneRepository
    {

        public CabinetPhoneRepository(string connectionString, IRepositoryContextFactory contextFactory) : base(connectionString, contextFactory) { }
        public async Task<CabinetPhone> GetCabinetPhone(int cabinetPhoneId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                CabinetPhone cabinetPhone = context.CabinetPhones.FirstOrDefault(cp => cp.Id == cabinetPhoneId);

                return cabinetPhone;
            }
        }

        public List<CabinetPhone> GetAllCabinetPhones()
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                List<CabinetPhone> cabinetPhones = context.CabinetPhones.ToList();

                return cabinetPhones;
            }
        }
    }
}
