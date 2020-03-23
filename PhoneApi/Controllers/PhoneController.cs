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
    public class PhoneController : ControllerBase
    {
        private readonly IPhoneService phoneService;
        private readonly ICabinetService cabinetService;

        public PhoneController(IPhoneService service, ICabinetService cabinetService)
        {
            phoneService = service;
            this.cabinetService = cabinetService;
        }

        [HttpGet]
        public async Task<List<Phone>> GetPhones()
        {   
            return await phoneService.GetAllPhones();
        }

        [HttpGet("{id}")]
        public async Task<Phone> GetPhone(int id)
        {
            return await phoneService.GetPhone(id);
        }

        [HttpPost]
        public async Task<ActionResult<Phone>> AddPhone(Phone phone)
        {
            if (phone == null)
            {
                return BadRequest();
            }
            await phoneService.AddPhone(phone);
            return Ok(phone);
        }

        [HttpPost("{phoneId}/{cabinetId}")]
        public async Task AddCabinetToPhone(int phoneId, int cabinetId)
        {
            Phone phone = await phoneService.GetPhone(phoneId);
            Cabinet cabinet = await cabinetService.GetCabinet(cabinetId);
            await phoneService.AddCabinetToPhone(phone, cabinet);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Phone>> Delete(int id)
        {
            Phone phone = await phoneService.GetPhone(id);
            if (phone == null)
            {
                return BadRequest();
            }
            await phoneService.DeletePhone(id);
            return Ok(phone);
        }
    }
}
