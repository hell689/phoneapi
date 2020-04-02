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
        IPhoneRepository phoneRepository;
        ICabinetRepository cabinetRepository;

        public CabinetPhoneService (ICabinetPhoneRepository repository, IPhoneRepository phoneRepository, ICabinetRepository cabinetRepository)
        {
            _repository = repository;
            this.cabinetRepository = cabinetRepository;
            this.phoneRepository = phoneRepository;
        }

        public async Task<List<CabinetPhone>> GetAllCabinetPhones()
        {
            List<CabinetPhone> cabinetPhones = _repository.GetAllCabinetPhones();
            foreach (CabinetPhone cabinetPhone in cabinetPhones)
            {
                cabinetPhone.Phone = await phoneRepository.GetPhone(unchecked((int)cabinetPhone.PhoneId));
                cabinetPhone.Cabinet = await cabinetRepository.GetCabinet(unchecked((int)cabinetPhone.CabinetId));
            }
            return cabinetPhones;
        }

        public async Task<CabinetPhone> GetCabinetPhone(int cabinetPhoneId)
        {
            CabinetPhone cabinetPhone = await _repository.GetCabinetPhone(cabinetPhoneId);
            cabinetPhone.Phone = await phoneRepository.GetPhone(unchecked((int)cabinetPhone.PhoneId));
            cabinetPhone.Cabinet = await cabinetRepository.GetCabinet(unchecked((int)cabinetPhone.CabinetId));

            return cabinetPhone;
        }
    }
}
