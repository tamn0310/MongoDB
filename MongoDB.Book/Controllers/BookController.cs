using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MongoDB.Book.Controllers
{
    [Route("api")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("books")]
        public ActionResult<List<Models.Book>> Get() =>
            _bookService.Gets();

        [HttpGet("books/{id:length(24)}", Name = "GetBook")]
        public ActionResult<Models.Book> Get(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost("books")]
        public ActionResult<Models.Book> Create(Models.Book book)
        {
            _bookService.Insert(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }

        [HttpPut("books/{id:length(24)}")]
        public IActionResult Update(string id, Models.Book bookIn)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Update(id, bookIn);

            return NoContent();
        }

        [HttpDelete("books/{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Delete(book.Id);

            return NoContent();
        }
    }
}