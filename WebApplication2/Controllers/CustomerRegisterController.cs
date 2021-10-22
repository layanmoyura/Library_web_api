using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Model;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CustomerRegisterController : ControllerBase
    {
        

        private readonly IMapper mapper;
        private readonly UserManager<Customer> UserManager;

        public CustomerRegisterController(IMapper mapper, UserManager<Customer> userManager)
        {
            this.mapper = mapper;
            UserManager = userManager;
        }

        [HttpPost]
        
        public async Task<ActionResult> Register(CustomerRegisterModel model)
        {
            var user = mapper.Map<Customer>(model);
            var result = await UserManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return StatusCode(201);
            }
            return StatusCode(400);

        }
    }
}
