using PhoneApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.Services.Interfaces
{
    public interface IPhoneService
    {
        Task<Phone> GetPhone(int phoneId);
        Task AddPhone(Phone phone);
        Task<List<Phone>> GetAllPhones();
        Task DeletePhone(int phoneId);
    }
}
