using Microsoft.Extensions.Configuration;
using PhoneApi.DBRepository.Interfaces;
using PhoneApi.Models;
using PhoneApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.Services
{
    public class PhoneService : IPhoneService
    {
        IPhoneRepository _repository;
        IConfiguration _config;

        public PhoneService(IPhoneRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }
        public async Task AddPhone(Phone phone)
        {
            await _repository.AddPhone(phone);
        }

        public async Task<List<Phone>> GetAllPhones()
        {
            var phones = await _repository.GetAllPhones();
            return phones;
        }

        public async Task<Phone> GetPhone(int phoneId)
        {
            var result = await _repository.GetPhone(phoneId);
            return result;
        }

        public async Task DeletePhone(int phoneId) {
            await _repository.DeletePhone(phoneId);
        }
    }
}
