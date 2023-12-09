using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Datalayer.Queries;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        public AccountController(ILogger<AccountController> logger)
        {
            this._logger = logger;
        }

        [HttpGet("GetByEmail/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Dictionary<string, string>>> GetAsync(string email)
        {
            if (email == null) { return BadRequest(); }
            Dictionary<string, string> account = await AccountQuery.ReadAccountByEmail(email);
            if (!account.ContainsKey("accounts_id")) { return NotFound(); }
            return Ok(account);
        }
    }
}
