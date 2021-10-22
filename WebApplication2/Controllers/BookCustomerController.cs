using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Model;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCustomerController : ControllerBase
    {

        private readonly DatabaseContext _context;
        public BookCustomerController(DatabaseContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "user")]
        [HttpGet("showbook")]
        public async Task<ActionResult<IEnumerable<Book>>> Getbook()
        {
            return await _context.Books.ToListAsync();
        }



        [Authorize(Roles = "user")]
        [HttpGet("searchbook/category/")]
        public async Task<ActionResult<IEnumerable<Book>>> GetPassCandidates(string bycategory)
        {
            return await _context.Books.Where(s => s.Category == bycategory).ToListAsync();
        }



    }
}
