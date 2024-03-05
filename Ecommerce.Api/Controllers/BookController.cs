using Ecommerce.Application.Services;
using Ecommerce.DTOs.Book;
using Ecommerce.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookServices;

        private readonly IAuthorService _authorServices;
        public BookController(IBookService BookService, IAuthorService AuthorServices)
        {
            _bookServices = BookService;
            _authorServices = AuthorServices;
        }
        // GET: BookController
        [HttpGet]
        public async Task<IActionResult> GetAll()
        { 
            try
            {
                var Query = await _bookServices.GetAllPagination(5, 1);
                if (Query.Count == 0)
                {
                    return NoContent();
                }
                return Ok(Query);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id:int}")]
        //[Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var Prd = await _bookServices.GetOne(id);
            if (Prd is null)
            {
                return Ok("NotFound");
            }
            return Ok(Prd);
        }
        [HttpPost]
        public IActionResult Create(CreateUpdateDeleteDto book)
        {
            if (ModelState.IsValid)
            {
                _bookServices.Create(book);
                //url.link()
                return Created("http://localhost:5164/api/Book/" + book.Id, "Saved");
            }
            return BadRequest(ModelState);
        }
        [HttpPut]
        public async Task<IActionResult> Update(CreateUpdateDeleteDto book)
        {

            if (ModelState.IsValid)
            {
                await _bookServices.UpdateAsync(book);
                //url.link()
                return Created("http://localhost:5164/api/Book/" + book.Id, "Saved");
            }
            return BadRequest(ModelState);
        }
    }
}
