using PhoneApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.DBRepository.Interfaces
{
    public interface ICabinetRepository
    {
        Task<Cabinet> GetCabinet(int cabinetId);
        Task AddCabinet(Cabinet cabinet);
        Task<List<Cabinet>> GetAllCabinets();
        Task DeleteCabinet(int cabinetId);
    }
}
