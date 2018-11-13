using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Core.Models.BookAggregate;
using Books.Core.Models.Fields;
using Books.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IBookRepository _repo;

        public ValuesController(IBookRepository repo)
        {
            _repo = repo;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<string>> Get(int id)
        {

            var book = new Book("Reconocerme 2");

            var newBook = await _repo.AddAsync(book);

            var chapter = newBook.AppendChapter("prueba");

            var pageFields = new List<PageField>() {
                new PageField(FieldType.Input, "Nombre", "Nombre", true),
                new PageField(FieldType.Picture, "Foto", "Foto", true),
                new PageField(FieldType.Text, "Acerca de mi", "Acerca_de_mi", true),
            };

            chapter.AddPage(pageFields);

            await _repo.UnitOfWork.CommitChangesAsync();

            var ok = await _repo.GetAsync(newBook.Id);

            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
