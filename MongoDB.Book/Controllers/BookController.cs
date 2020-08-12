using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Book.Models;

namespace MongoDB.Book.Controllers
{
    [Route("api")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookContext _bookContext;
        public BookController(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        [HttpGet("books")]
        public async Task<ActionResult<IEnumerable<Models.Book>>> Gets()
        {
            return await _bookContext.Books.ToListAsync();
        }
        [HttpGet("books/{id:int}")]
        public async Task<ActionResult<Models.Book>> Get(int id)
        {
            var todoItem = await _bookContext.Books.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return todoItem;
        }

        [HttpPost("books")]
        public async Task<ActionResult<Models.Book>> Post(Models.Book book)
        {
            _bookContext.Books.Add(book);
            await _bookContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }

    }
}
