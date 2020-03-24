using PhoneApi.DBRepository.Interfaces;
using PhoneApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace PhoneApi.DBRepository.Repositories
{
    public class PhoneRepository : BaseRepository, IPhoneRepository
    {
        public PhoneRepository(string connectionString, IRepositoryContextFactory contextFactory) : base(connectionString, contextFactory) { }
        public async Task AddPhone(Phone phone)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                context.Phones.Add(phone);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Phone>> GetAllPhones()
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                List<Phone> phones = context.Phones.ToList();
                foreach (Phone phone in phones)
                {
                    List<Cabinet> cabinets = new List<Cabinet>();
                    List<CabinetPhone> cabinetPhones = context.CabinetPhones.FromSqlRaw("SELECT phoneId, cabinetId FROM CabinetPhone WHERE phoneId = {0}", phone.Id).ToList();
                    foreach (var cabinetPhone in cabinetPhones)
                    {
                        Cabinet cabinet = context.Cabinets.FirstOrDefault(c => c.Id == cabinetPhone.CabinetId);
                        cabinet.CabinetPhones = new List<CabinetPhone>();
                        cabinets.Add(cabinet);
                    }
                    phone.CabinetPhones = new List<CabinetPhone>();
                    phone.Cabinets = cabinets;
                        
                }
                return phones;
            }
        }

        public async Task<Phone> GetPhone(int phoneId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                Phone phone = await context.Phones.FirstOrDefaultAsync(p => p.Id == phoneId);
                List<Cabinet> cabinets = new List<Cabinet>();
                List<CabinetPhone> cabinetPhones = context.CabinetPhones.FromSqlRaw("SELECT phoneId, cabinetId FROM CabinetPhone WHERE phoneId = {0}", phone.Id).ToList();
                foreach (var cabinetPhone in cabinetPhones)
                {
                    Cabinet cabinet = context.Cabinets.FirstOrDefault(c => c.Id == cabinetPhone.CabinetId);
                    cabinet.CabinetPhones = new List<CabinetPhone>();
                    cabinets.Add(cabinet);
                }
                phone.CabinetPhones = new List<CabinetPhone>();
                phone.Cabinets = cabinets;
                return phone;
            }
        }

        public async Task DeletePhone(int phoneId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var phone = new Phone() { Id = phoneId };
                context.Phones.Remove(phone);
                await context.SaveChangesAsync();
            }
        }

        public async Task AddCabinetToPhone(Phone phone, Cabinet cabinet)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                CabinetPhone cabinetPhone = new CabinetPhone();
                cabinetPhone.Cabinet = cabinet;
                cabinetPhone.CabinetId = cabinet.Id;
                cabinetPhone.Phone = phone;
                cabinetPhone.PhoneId = phone.Id;
                context.Database.ExecuteSqlRaw("INSERT INTO CabinetPhone (CabinetId, PhoneId) VALUES ({0}, {1})", cabinetPhone.CabinetId, cabinetPhone.PhoneId);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteCabinetFromPhone(Phone phone, Cabinet cabinet)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                context.Database.ExecuteSqlRaw("DELETE FROM CabinetPhone WHERE CabinetId={0} AND PhoneId={1}", cabinet.Id, phone.Id);
                await context.SaveChangesAsync();
            }
        }
    }
}
