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
    public class BookAdminController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public BookAdminController(DatabaseContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("addorupdatebook")]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            try
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet("showbook")]
        public async Task<ActionResult<IEnumerable<Book>>> Getbook()
        {
            return await _context.Books.ToListAsync();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("searchbook/id/{id}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetCandidate(int id)
        {
            return await _context.Books.Where(s => s.BookID == id).ToListAsync();
        }


        /*[HttpPut("addbookorupdate/{id}")]
        public async Task<IActionResult> PutCandidate(int id, Book book)
        {
            if (id != book.BookID)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return StatusCode(201);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }*/
        [Authorize(Roles = "admin")]
        [HttpPut("addorupdatebook/{id}")]
        public async Task<ActionResult> Update(int id,Book book)
        {
            if (id != book.BookID)
            {
                return BadRequest();
            }
            try
            {
                var Entity = await _context.Books.FindAsync(id);
                if (!string.IsNullOrEmpty(book.BookName)) { Entity.BookName = book.BookName; }
                if (!string.IsNullOrEmpty(book.Author)) { Entity.Author = book.Author; }
                if (!string.IsNullOrEmpty(book.Category)) { Entity.Category = book.Category; }
                if (!string.IsNullOrEmpty(book.Editon)) { Entity.Editon = book.Editon; }
                if (!string.IsNullOrEmpty(book.LastEditDate)) { Entity.LastEditDate = book.LastEditDate; }
                if (!string.IsNullOrEmpty(book.Link)) { Entity.Link = book.Link; }
                if (!string.IsNullOrEmpty(book.CoverImage)) { Entity.CoverImage = book.CoverImage; }

                await _context.SaveChangesAsync();
                return StatusCode(202);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


        }


        [Authorize(Roles = "admin")]
        [HttpGet("searchbook/category/")]
        public async Task<ActionResult<IEnumerable<Book>>> GetPassCandidates(string bycategory)
        {
            return await _context.Books.Where(s => s.Category == bycategory).ToListAsync();
        }


        [Authorize(Roles = "admin")]
        [HttpDelete("deletebook/{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return StatusCode(202);
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookID == id);
        }
    }
}



