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

        //[Route("phone")]
        [HttpGet]
        public async Task<List<Phone>> GetPhones()
        {   
            return await phoneService.GetAllPhones();
        }

        //[Route("phone")]
        [HttpGet("{id}")]
        public async Task<Phone> GetPhone(int id)
        {
            return await phoneService.GetPhone(id);
        }

        //[Route("phone")]
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
