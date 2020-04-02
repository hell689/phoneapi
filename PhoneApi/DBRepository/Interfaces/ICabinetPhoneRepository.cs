using PhoneApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.DBRepository.Interfaces
{
    public interface ICabinetPhoneRepository
    {
        Task<CabinetPhone> GetCabinetPhone(int cabinetPhoneId);

        List<CabinetPhone> GetAllCabinetPhones();
    }
}
