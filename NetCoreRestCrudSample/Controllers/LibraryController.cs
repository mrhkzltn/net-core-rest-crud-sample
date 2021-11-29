using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreRestCrudSample.Data;
using NetCoreRestCrudSample.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetCoreRestCrudSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {

        private ApiDbContext _dbContext;

        public LibraryController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<LibraryController>?pageNumber=1&pageSize=5
        [HttpGet]
        public IActionResult Get(int? pageNumber, int? pageSize)
        {
            int currentPageNumber = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 5;
            return Ok(_dbContext.Libraries.Skip((currentPageNumber - 1)* currentPageSize).Take(currentPageSize));
        }

        // GET api/<LibraryController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Library? library = _dbContext.Libraries.Find(id);
            return Ok(library);
        }

        // POST api/<LibraryController>
        [HttpPost]
        public IActionResult Post([FromBody] Library library)
        {
            _dbContext.Libraries.Add(library);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);

        }

        // PUT api/<LibraryController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Library library)
        {
            Library? foundLibrary = _dbContext.Libraries.Find(id);
            if(foundLibrary == null)
            {
                return NotFound("Library not found!");
            }
            foundLibrary.Name = library.Name;
            foundLibrary.Books= library.Books;
            foundLibrary.City= library.City;
            _dbContext.SaveChanges();
            return Ok("update succeed!");
        }

        // DELETE api/<LibraryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Library? foundLibrary = _dbContext.Libraries.Find(id);
            if (foundLibrary == null)
            {
                return NotFound("Library not found!");
            }
            _dbContext.Libraries.Remove(foundLibrary);
            _dbContext.SaveChanges(true);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        
    }
}
