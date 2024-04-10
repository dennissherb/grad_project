using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Datalayer.Models;
using Datalayer.Repositories;
using DataObjects;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly IPageRepository _repository;

        public PagesController(IPageRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Page>>> GetPages()
        {
            var pages = await _repository.GetPagesAsync();
            return Ok(pages);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Page>> GetPage(int id)
        {
            var page = await _repository.GetPageByIdAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            return Ok(page);
        }

        [HttpPost]
        public async Task<ActionResult<Page>> CreatePage(Page page)
        {
            await _repository.CreatePageAsync(page);
            return CreatedAtAction(nameof(GetPage), new { id = page.Id }, page);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePage(int id,Page page)
        {
            if (page.Id == 0)
            {
                page.Id = id;
            }
            await _repository.UpdatePageAsync(page);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePage(int id)
        {
            await _repository.DeletePageAsync(id);
            return NoContent();
        }
    }
}
