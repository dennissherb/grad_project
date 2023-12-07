using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Datalayer.Queries;

namespace WebAPI.Controllers
{
    [Route("api/AccountController")]
    [ApiController]
    public class AccountActionsController : ControllerBase
    {
        private readonly ILogger<AccountActionsController> _logger;
        public AccountActionsController(ILogger<AccountActionsController> logger)
        {
            this._logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccountActionsController>> GetAccount()
        {
            //if (email == "") { return BadRequest(); }
            //Dictionary<string, string> account = await AccountQuery.ReadAccountByEmail(email);
            //if (!account.ContainsKey("id")) { return NotFound(); }
            //return Ok(account);
            return Ok("ok");
        }
    }
}
