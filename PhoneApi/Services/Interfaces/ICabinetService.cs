using PhoneApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.Services.Interfaces
{
    public interface ICabinetService
    {
        Task<Cabinet> GetCabinet(int id);
        Task AddCabinet(Cabinet cabinet);
        Task<List<Cabinet>> GetAllCabinets();
        Task DeleteCabinet(int Id);
    }
}
