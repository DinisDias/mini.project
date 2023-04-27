using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini.Projeto.Models;
using Mini.Projeto.Repositories;
using Mini.Projeto.Repositories.Interfaces;

namespace Mini_Projeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<BookModel>>> SearchAllBooks()
        {
            List<BookModel> books = await _bookRepository.SearchAllBooks();
            return Ok(books);
        }

        [HttpGet("{isbn}")]
        public async Task<ActionResult<List<BookModel>>> SearchBookIsbn(string isbn)
        {
            BookModel book = await _bookRepository.SearchBookIsbn(isbn);
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookModel>> AddBook(BookModel book)
        {
            try
            {
                await _bookRepository.Add(book);
                return CreatedAtAction(nameof(SearchBookIsbn), new { isbn = book.isbn }, book);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("{isbn}")]
        public async Task<IActionResult> Put(string isbn, [FromBody] BookModel book)
        {
            if (isbn != book.isbn)
            {
                return BadRequest();
            }

            await _bookRepository.Update(isbn, book);
            return NoContent();
        }


        [HttpDelete("{isbn}")]
        public async Task<ActionResult<BookModel>> Delete(string isbn)
        {
            bool deleted = await _bookRepository.Delete(isbn);
            if (deleted)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}

