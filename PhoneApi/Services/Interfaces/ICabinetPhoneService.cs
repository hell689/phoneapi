using PhoneApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.Services.Interfaces
{
    public interface ICabinetPhoneService
    {
        Task<CabinetPhone> GetCabinetPhone(int cabinetPhoneId);
    }
}
