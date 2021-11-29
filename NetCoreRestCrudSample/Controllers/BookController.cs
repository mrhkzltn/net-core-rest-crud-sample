using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreRestCrudSample.Data;
using NetCoreRestCrudSample.Models;

namespace NetCoreRestCrudSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public BookController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {

            return Ok(_dbContext.Books);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = _dbContext.Books.Find(id);
            if(book == null)
            {
                return NotFound("Record not found!");
            }
            return Ok(book);
        }
        // api/Book/testrouting/1
        [HttpGet("[action]/{id}")]
        public int TestRouting(int id)
        {
            return id;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book book)
        {
            Book? foundBook = _dbContext.Books.Find(id);
            if (foundBook != null)
            {
                foundBook.Title = book.Title;
                foundBook.Content= book.Content;
                foundBook.Language = book.Language;
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return NotFound("record not found!");
            }
            
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Book? book = _dbContext.Books.Find(id);
            if(null == book)
            {
                return NotFound("record not found!");
            }
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> SearchBooks(string query)
        {
            var books = await (from book in _dbContext.Books
                               where book.Title.StartsWith(query)
                               select new
                               {
                                   Id = book.Id,
                                   Title = book.Title,
                                   LibraryId = book.LibraryId
                               }).Take(15).ToListAsync();
            return Ok(books);
        }
    }
}
