using PhoneApi.DBRepository.Interfaces;
using PhoneApi.Models;
using PhoneApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.Services
{
    public class CabinetPhoneService : ICabinetPhoneService
    {

        ICabinetPhoneRepository _repository;

        public CabinetPhoneService (ICabinetPhoneRepository repository)
        {
            _repository = repository;
        }
        public async Task<CabinetPhone> GetCabinetPhone(int cabinetPhoneId)
        {   
            return await _repository.GetCabinetPhone(cabinetPhoneId);
        }
    }
}
