using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UJ_AccountManagement.Domain.DTOs;
using UJ_AccountManagement.Domain.Entities;
using UJ_AccountManagement.Domain.Interfaces;

namespace UJ_AccountManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionsController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<TransactionDTO>>> GetAllTransactions()
        {
            var transactions = await _transactionRepository.GetAllTransactions();

            if (transactions == null || transactions.Count == 0) return NotFound();

            return Ok(transactions?? new List<Domain.DTOs.TransactionDTO>());
        }

        [HttpGet("{transactionId}")]
        public async Task<ActionResult<Transaction>> GetTransactionById(int transactionId)
        {
            // If transactionId is 0 or negative, return BadRequest
            if (transactionId <= 0)
                return BadRequest(new { message = "Invalid transaction ID!", status = "error" });

            // Fetch transaction from repository
            var transaction = await _transactionRepository.GetTransactionById(transactionId);

            // If not found, return NotFound
            if (transaction == null)
                return NotFound(new { message = "Transaction does not exist! \t :(", status = "error" });

            return Ok(transaction);
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<List<Transaction>>> GetTransactions(string searchTerm)
        {
            var transactions = await _transactionRepository.GetTransactions(searchTerm);

            if (transactions == null) return NotFound(new { message = "No transactions found! 😢", status = "error" });

            return Ok(transactions);
        }

        [HttpPost]
        public async Task<ActionResult> AddTransaction([FromBody] Transaction transaction)
        {
            if (transaction == null || !ModelState.IsValid) return BadRequest("Invalid model! 😢");

            await _transactionRepository.AddTransaction(transaction);

            var location = Url?.Action("GetTransactionById", "Transactions", new { transactionId = transaction.TransactionId }) ?? "/api/transactions";

            return Created(location, new { message = "Transaction added successfully! 😊", status = "success", transaction });
        }



        [HttpGet("searchByDate")]
        public async Task<ActionResult<List<Transaction>>> GetTransactionsByDate(DateTime? startDate, DateTime? endDate)
        {
            // If the endDate is not provided, use the current date
            if (!endDate.HasValue) endDate = DateTime.UtcNow.Date; // Sets to the current date

            // If the startDate is not provided, return a BadRequest
            if (!startDate.HasValue)return BadRequest(new { message = "Start date is required! 😢", status = "error" });

            var transactions = await _transactionRepository.GetTransactionsByDate(startDate, endDate);

            // If no transactions were found, return a NotFound response
            if (transactions == null || !transactions.Any()) return NotFound(new { message = "No transactions found for the given date range. 😢",
                status = "error" });

            return Ok(transactions);
        }

        [HttpGet("AccountHolder")]
        public async Task<ActionResult<List<Transaction>>> GetTransactionsByAccountHolder([FromQuery] string accountHolder)
        {
            if (string.IsNullOrWhiteSpace(accountHolder))
                return BadRequest(new { message = "Account holder name is required! 😢", status = "error" });

            var transactions = await _transactionRepository.GetTransactionsByAccountHolder(accountHolder);

            if (transactions == null || transactions.Count == 0)
                return NotFound(new { message = "No transactions found for this account holder! 😢", status = "error" });

            return Ok(transactions);
        }



        [HttpPut]
        public async Task<ActionResult> UpdateTransaction(Transaction transaction)
        {
            if(!ModelState.IsValid) return BadRequest(new { message="Invalid input! 😢", status = "error" });
            var existingTransaction = await _transactionRepository.GetTransactionById(transaction.TransactionId);
            if (existingTransaction == null) return NotFound(new { message = "Transaction does not exist! 😢", status = "error" });

            await _transactionRepository.UpdateTransaction(transaction);
            return Ok(new { message= "Transaction updated successfully! 😊", status="success", transaction=transaction });
        }

        [HttpDelete]
        [Route("{transactionId}")]
        public async Task<ActionResult> DeleteTransaction(int transactionId)
        {
            var existingTransaction = await _transactionRepository.GetTransactionById(transactionId);
            if (existingTransaction == null) return NotFound(new { message = "Transaction does not exist! 😢", status="error" });

            await _transactionRepository.DeleteTransaction(transactionId);

            return Ok(new { message = "Transaction deleted successfully! 😊", status="success" });
        }

        [HttpGet("CustomFilter")]
        public async Task<ActionResult<List<Transaction>>> GetFilteredTransactions(string? accountHolder, int? accountId, 
                                                                                   string? transactionType, DateTime? startDate, DateTime? endDate)
        {
            var transactions = await _transactionRepository.GetFilteredTransactions(accountHolder, accountId, transactionType, startDate, endDate);

            if (transactions == null || transactions.Count == 0)
                return NotFound(new { message = "No transactions found with the given filters! 😢", status = "error" });

            return Ok(transactions);
        }


    }
}
