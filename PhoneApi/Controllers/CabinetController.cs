using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneApi.Models;
using PhoneApi.Services.Interfaces;

namespace PhoneApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CabinetController : ControllerBase
    {
        private readonly ICabinetService cabinetService;

        public CabinetController(ICabinetService service)
        {
            cabinetService = service;
        }

        [HttpGet]
        public async Task<List<Cabinet>> GetCabinets()
        {   
            return await cabinetService.GetAllCabinets();
        }

        [HttpGet("{id}")]
        public async Task<Cabinet> GetCabinet(int id)
        {
            return await cabinetService.GetCabinet(id);
        }

        [HttpPost]
        public async Task<ActionResult<Cabinet>> AddCabinet(Cabinet cabinet)
        {
            if (cabinet == null)
            {
                return BadRequest();
            }
            await cabinetService.AddCabinet(cabinet);
            return Ok(cabinet);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Cabinet>> Delete(int id)
        {
            Cabinet cabinet = await cabinetService.GetCabinet(id);
            if (cabinet == null)
            {
                return BadRequest();
            }
            await cabinetService.DeleteCabinet(id);
            return Ok(cabinet);
        }
    }
}
