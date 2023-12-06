using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Datalayer.Queries;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountActionsController : ControllerBase
    {
        private readonly ILogger<AccountActionsController> _logger;
        public AccountActionsController(ILogger<AccountActionsController> logger)
        {
            this._logger = logger;
        }

        [HttpGet("{email:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AccountActionsController> Get(long uid, string pass)
        {
            if (uid <= 0) { return BadRequest(); }
            Dictionary<string, string> account = AccountQuery.ReadAccountByEmail(email) ;
            if (bu == null) { return NotFound(); }
            return Ok(bu);
        }



    }
}
