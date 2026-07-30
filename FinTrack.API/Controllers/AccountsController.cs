using FinTrack.API.Data;
using FinTrack.API.Models.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly FinTrackDbContext dbContext;
        public AccountsController(FinTrackDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllAccounts()
        {
            var accounts = dbContext.Accounts.ToList();

            return Ok(accounts);
        }

        [HttpGet]
        [Route("{id:guid}")]

        public IActionResult GetAccountById([FromRoute] Guid id)
        {
            var account = dbContext.Accounts.FirstOrDefault(x => x.Id == id);

            if (account == null) return NotFound();

            return Ok(account);
        }

        [HttpGet]
        [Route("search")]
        public IActionResult GetAccountByName([FromQuery] string accountName) 
        {
            var accounts = dbContext.Accounts
                .Where(x=>x.Name.ToLower().Contains(accountName.ToLower()))
                .ToList();

            if (!accounts.Any()) 
            {
                return NotFound(new { Message = "No matching account found." });
            }

            if (string.IsNullOrWhiteSpace(accountName))
            {
                return BadRequest(new { Message = "Account name is required." });
            }

            return Ok(accounts);
        }

    }
}
