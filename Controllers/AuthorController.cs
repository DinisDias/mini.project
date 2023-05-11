using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini.Projeto.Models;
using Mini.Projeto.Repositories.Interfaces;
using Mini.Projeto.Models;
using Mini.Projeto.Repositories;
using Mini.Projeto.Repositories.Interfaces;
using static Mini.Projeto.Models.AuthorModel;

namespace Mini_Projeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<BookModel>>> SearchAllAuthors()
        {
            List<AuthorModel> author = await _authorRepository.SearchAllAuthors();
            return Ok(author);
        }

        [HttpGet("{idAuthor}")]
        public async Task<ActionResult<List<AuthorModel>>> SearchAuthorId(int idAuthor)
        {
            AuthorModel author = await _authorRepository.SearchAuthorId(idAuthor);
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorModel>> AddAuthor(AuthorModel author)
        {
            try
            {
                await _authorRepository.Add(author);
                return CreatedAtAction("SearchAuthorId", new { idAuthor = author.idAuthor }, author);

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


        [HttpPut("{idAuthor}")]
        public async Task<IActionResult> Put(int idAuthor, [FromBody] AuthorModel author)
        {
            if (idAuthor != author.idAuthor)
            {
                return BadRequest();
            }

            await _authorRepository.Update(idAuthor, author);
            return NoContent();
        }


        [HttpDelete("{idAuthor}")]
        public async Task<ActionResult<AuthorModel>> Delete(int idAuthor)
        {
            bool deleted = await _authorRepository.Delete(idAuthor);
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