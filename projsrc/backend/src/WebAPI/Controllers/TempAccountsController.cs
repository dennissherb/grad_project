using Datalayer.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempAccountsController : ControllerBase
    {
        private readonly ILogger<TempAccountsController> _logger;
        public TempAccountsController(ILogger<TempAccountsController> logger)
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

        [HttpGet("GetByID/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dictionary<string, string>>> GetAsync(int id)
        {
            if (id <= 0) { return BadRequest(); }
            Dictionary<string, string> account = await AccountQuery.ReadAccountById(id);
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
                if(await AccountQuery.DeleteAccount(user))
                    return Ok();
            }
            return StatusCode(500, "An unknown error has occurred");
        }

        [HttpPost("update_user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        //oldUser must contain at least one UQ to determine which entry to edit
        public async Task<ActionResult<Dictionary<string,string>>> TryUpdateUser([FromBody] Dictionary<string, string> user)
        {

            if (user == null)
                return BadRequest();
            if (await (AccountQuery.ReadAccountByIdAsync(user)) == null)
                return NotFound();
            try
            {
                user = await AccountQuery.UpdateAccount(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error has occurred: {ex.Message}");
            }
            return Ok(user);
        }
    }
}
