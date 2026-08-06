using FinTrack.API.Data;
using FinTrack.API.Models.Domains;
using FinTrack.API.Models.DTO;
using FinTrack.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FinTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly FinTrackDbContext dbContext;
        private readonly IAccountRepository accountRepository;
        public AccountsController(FinTrackDbContext dbContext, IAccountRepository accountRepository)
        {
            this.dbContext = dbContext;
            this.accountRepository = accountRepository;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {

            var accountsDomain = await accountRepository.GetAllAccountsAsync();

            var accountsDto = new List<AccountDto>();

            foreach (var account in accountsDomain)
            {
                accountsDto.Add(new AccountDto
                {
                    Id = account.Id,
                    Name = account.Name,
                    AccountNumber = account.AccountNumber,
                    Balance = account.Balance,
                    Currency = account.Currency,
                });
            }

            return Ok(accountsDto);
        }

        [HttpGet]
        [Route("{id:guid}")]

        public async Task<IActionResult> GetAccountById([FromRoute] Guid id)
        {
            var accountDomain = await dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == id);

            if (accountDomain == null) return NotFound();

            var accountDto = new AccountDto
            {
                Id = accountDomain.Id,
                Name = accountDomain.Name,
                AccountNumber = accountDomain.AccountNumber,
                Balance = accountDomain.Balance,
                Currency = accountDomain.Currency,
            };

            return Ok(accountDto);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetAccountByName([FromQuery] string accountName)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                return BadRequest(new { Message = "Account name is required." });
            }

            var accountsDomain = await dbContext.Accounts
                .Where(x=>x.Name.ToLower().Contains(accountName.ToLower()))
                .ToListAsync();

            if (!accountsDomain.Any()) 
            {
                return NotFound(new { Message = "No matching account found." });
            }

            var accountsDto = new List<AccountDto>();

            foreach (var account in accountsDomain) {
                accountsDto.Add( new AccountDto
                {
                    Id = account.Id,
                    Name = account.Name,
                    AccountNumber = account.AccountNumber,
                    Balance = account.Balance,
                    Currency = account.Currency,

                });
            }

            return Ok(accountsDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AddAccountRequestDto addAccountRequestDto)
        {
            var accountDomainModel = new Account
            {
                Name = addAccountRequestDto.Name,
                AccountNumber = addAccountRequestDto.AccountNumber,
                Balance = addAccountRequestDto.Balance,
                Currency = addAccountRequestDto.Currency.ToUpper(),
            };

            await dbContext.Accounts.AddAsync(accountDomainModel);
            await dbContext.SaveChangesAsync();

            var accountDto = new AccountDto
            {
                Id = accountDomainModel.Id,
                Name = accountDomainModel.Name,
                AccountNumber = accountDomainModel.AccountNumber,
                Balance = accountDomainModel.Balance,
                Currency = accountDomainModel.Currency,

            };

            return CreatedAtAction(nameof(GetAccountById), new { id = accountDto.Id }, accountDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateAccount([FromRoute] Guid id, [FromBody] UpdateAccountRequestDto updateAccountRequestDto)
        {
            var accountDomainModel = await dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == id);

            if (accountDomainModel == null) return NotFound();

            accountDomainModel.Name = updateAccountRequestDto.Name;
            accountDomainModel.AccountNumber = updateAccountRequestDto.AccountNumber;
            accountDomainModel.Balance = updateAccountRequestDto.Balance;

            await dbContext.SaveChangesAsync();

            var accountDto = new AccountDto
            {
                Id = accountDomainModel.Id,
                Name = accountDomainModel.Name,
                AccountNumber = accountDomainModel.AccountNumber,
                Balance = accountDomainModel.Balance,
                Currency = accountDomainModel.Currency,
            };

            return Ok(accountDto);
        }
        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteAccount([FromRoute] Guid id)
        {
            var accountDomainModel = await dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == id);

            if (accountDomainModel == null) return NotFound();

            dbContext.Accounts.Remove(accountDomainModel);

            await dbContext.SaveChangesAsync();

            var accountDto = new AccountDto
            {
                Id = accountDomainModel.Id,
                Name = accountDomainModel.Name,
                AccountNumber = accountDomainModel.AccountNumber,
                Balance = accountDomainModel.Balance,
                Currency = accountDomainModel.Currency,
            };

            return Ok(accountDto);
        }

    }
}
