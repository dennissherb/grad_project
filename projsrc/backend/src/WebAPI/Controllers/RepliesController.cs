using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Datalayer.Repositories;
using DataObjects;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepliesController : ControllerBase
    {
        private readonly IReplyRepository _repository;

        public RepliesController(IReplyRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reply>>> GetReplies()
        {
            var replies = await _repository.GetRepliesAsync();
            return Ok(replies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reply>> GetReply(int id)
        {
            var reply = await _repository.GetReplyByIdAsync(id);
            if (reply == null)
            {
                return NotFound();
            }
            return Ok(reply);
        }

        [HttpGet("ByAuthor/{id}")]
        public async Task<ActionResult<List<Reply>>> GetRepliesByAuthor(int id)
        {
            var replies = await _repository.GetRepliesByAuthorAsync(id);
            return Ok(replies);
        }

        [HttpGet("ByPage/{id}")]
        public async Task<ActionResult<List<Reply>>> GetRepliesByPage(int id)
        {
            var replies = await _repository.GetRepliesByPageAsync(id);
            return Ok(replies);
        }

        [HttpPost]
        public async Task<ActionResult<Reply>> CreateReply(Reply reply)
        {
            await _repository.CreateReplyAsync(reply);
            return CreatedAtAction(nameof(GetReply), new { id = reply.Id }, reply);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReply(int id, Reply reply)
        {
            if (reply.Id == null || reply.Id != id)
            {
                return BadRequest();
            }

            await _repository.UpdateReplyAsync(reply);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReply(int id)
        {
            await _repository.DeleteReplyAsync(id);
            return NoContent();
        }
    }
}
