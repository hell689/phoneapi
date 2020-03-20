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
    public class CabinetService : ICabinetService
    {
        ICabinetRepository _repository;
        IConfiguration _config;

        public CabinetService(ICabinetRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }
        public async Task AddCabinet(Cabinet cabinet)
        {
            await _repository.AddCabinet(cabinet);
        }

        public async Task<List<Cabinet>> GetAllCabinets()
        {
            var cabinets = await _repository.GetAllCabinets();
            return cabinets;
        }

        public async Task<Cabinet> GetCabinet(int Id)
        {
            var result = await _repository.GetCabinet(Id);
            return result;
        }

        public async Task DeleteCabinet(int Id)
        {
            await _repository.DeleteCabinet(Id);
        }
    }
}
