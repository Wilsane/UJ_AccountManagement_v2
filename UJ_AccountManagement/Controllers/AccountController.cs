using Microsoft.AspNetCore.Mvc;
using UJ_AccountManagement.Domain.DTOs;
using UJ_AccountManagement.Domain.Entities;
using UJ_AccountManagement.Domain.Interfaces;

namespace UJ_AccountManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAccount(Account account)
        {
            if (account == null || !ModelState.IsValid) return BadRequest("Invalid model! 😢");
            await _accountRepository.CreateAccount(account);
            var location = Url?.Action("GetAccountById", "Account", new { accountId = account.AccountId }) ?? "/api/account";

            return Created(location, new { message = "Account created successfully! 😊", status = "success", account });
        }


        [HttpDelete]
        [Route("{accountId}")]
        public async Task<ActionResult> DeleteAccount(int accountId)
        {
            var existingTransaction = await _accountRepository.GetAccountById(accountId);
            if (existingTransaction == null) return NotFound(new { message = "Account does not exist! 😢", status = "error" });

            await _accountRepository.DeleteAccount(accountId);

            return Ok(new { message = "Account deleted successfully! 😊", status = "success" });
        }


        [HttpGet("filter")]
        public async Task<IActionResult> FilterAccounts(string? accountHolder, string? email, string? phone, decimal? 
            minBalance, decimal? maxBalance, DateTime? createdFrom, DateTime? createdTo)
        {
            var result = await _accountRepository.FilterAccounts(accountHolder, email,phone,minBalance,maxBalance, createdFrom, createdTo);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<Account>>> GetAllAccounts()
        {
            var accounts = await _accountRepository.GetAllAccounts();
            if (accounts == null || !accounts.Any()) return NotFound(new { message = "No accounts found! 😢", status = "error" });
            return Ok(accounts);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAccount(Account account)
        {
            if (!ModelState.IsValid) return BadRequest(new { message = "Invalid input! 😢", status = "error" });
            var existingAccount = await _accountRepository.GetAccountById(account.AccountId);
            if (existingAccount == null) return NotFound(new { message = "Account does not exist! 😢", status = "error" });
            await _accountRepository.UpdateAccount(account);
            return Ok(new { message = "Account updated successfully! 😊", status = "success", account });
        }
    }
}
