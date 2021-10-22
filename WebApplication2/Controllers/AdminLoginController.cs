using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Model;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminLoginController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public AdminLoginController(DatabaseContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult> Login(AdminLogin model)
        {

            try
            {
                var admin = await _context.Admins.FindAsync(model.ID);
                var adminpass = await _context.Admins.Where(o => o.Password == model.Password).ToArrayAsync();


                if(adminpass.Length == 0)
                {
                    return Unauthorized("Invaild id or password");
                }

                

                else if ((admin != null) && (admin.ID == adminpass[0].ID))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(
                        issuer: "http://localhost:5000",
                        audience: "http://localhost:5000",
                        claims: new List<Claim>() {
                        new Claim("role","admin")},
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return Ok(new { Token = tokenString });
                }

                return Unauthorized("Invaild id or password");
            }

            catch(Exception ex)
            {
                throw ex;
            }

        }

    }
}
