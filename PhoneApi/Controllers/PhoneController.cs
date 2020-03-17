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

        public PhoneController(IPhoneService service)
        {
            phoneService = service;
        }

        [Route("phone")]
        [HttpGet]
        public async Task<List<Phone>> GetPhones()
        {   
            return await phoneService.GetAllPhones();
        }

        [Route("phone")]
        [HttpGet]
        public async Task<Phone> GetPhone(int id)
        {
            Phone phone = new Phone();
            phone.Id = 1;
            phone.PhoneNumber = "252-547-85";
            return phone;
            //return await phoneService.GetPhone(id);
        }

        [Route("phone")]
        [HttpPost]
        public async Task AddPhone([FromBody] Phone phone)
        {
            await phoneService.AddPhone(phone);
        }
    }
}
