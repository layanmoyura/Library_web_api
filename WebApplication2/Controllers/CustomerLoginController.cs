using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication2.Model;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerLoginController : ControllerBase
    {
       
        private readonly UserManager<Customer> UserManager;

        public CustomerLoginController(IMapper mapper, UserManager<Customer> userManager)
        {
            
            UserManager = userManager;
        }

        [Authorize]
        [HttpGet("test")]       //api/customer/test
       public String test()
        {
            return "Hello-world";
        }

       

        [HttpPost]
        public async Task<ActionResult> Login(CustomerLoginModel model)
        {
            var user = await UserManager.FindByEmailAsync(model.Email);

            if(user != null && await UserManager.CheckPasswordAsync(user, model.Password))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:5000",
                    audience: "http://localhost:5000",
                    claims: new List<Claim>() { 
                    new Claim("role","user"),new Claim("name",user.FirstName)},
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }

            return Unauthorized("Invaild email or password");

        }
    }
}
