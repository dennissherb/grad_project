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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dictionary<string, string>>> GetAsync(string email)
        {
            if (email == null) { return BadRequest(); }
            Dictionary<string, string> account = await AccountQuery.ReadAccountByEmail(email);
            if (account == null || !account.ContainsKey("accounts_id")) { return NotFound(); }
            return Ok(account);
        }

        [HttpGet("GetEntireTable")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Dictionary<string, string>>> GetEntireTable()
        {
            List<Dictionary<string, string>> table = await AccountQuery.ReadAccounts();
            if(table.Count < 0) return NotFound();
            return Ok(table);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Dictionary<string,string>>> TryLogin([FromBody] Dictionary<string,string> user ) {
            if (user["accounts_email"] == null && user["accounts_user_name"] == null || user["accounts_password"] == null) return BadRequest();
            if (await AccountQuery.TryLogin(user)) {
                user = await AccountQuery.ReadAccountByUQ(user);
                return Ok(user);
            }
            return Unauthorized();
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Dictionary<string,string>>> TryRegister([FromBody] Dictionary<string,string> user) {
            if (user["accounts_email"] == null || user["accounts_password"] == null) return BadRequest();
            Dictionary<string, string> userCheck = await AccountQuery.ReadAccountByUQ(user);
            if (userCheck != null && userCheck.Count != 0)
            {
                 return BadRequest();
            }
            if (await AccountQuery.CreateAccount(user))
                return Ok(user);
            else
                return StatusCode(500, $"Error with account insert");
        }
        
        [HttpPost("delete_as_admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> TryDeleteByAdmin([FromBody] Dictionary<string,string> user)
        {
            if (user["accounts_email"] == null) return BadRequest();
            Dictionary<string, string> userCheck = await AccountQuery.ReadAccountByUQ(user);
            if (userCheck == null || userCheck.Count == 0)
            {
                return NotFound();
            }
            else
            {
                if(await AccountQuery.DeleteAccountAsAdminByUQ(user));
                    return Ok();
            }
        }
    }
}
