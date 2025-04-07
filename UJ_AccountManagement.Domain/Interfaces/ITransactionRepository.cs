using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJ_AccountManagement.Domain.DTOs;
using UJ_AccountManagement.Domain.Entities;

namespace UJ_AccountManagement.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<TransactionDTO>> GetAllTransactions();
        Task<Transaction> GetTransactionById(int transactionId);

        Task<List<TransactionDTO>> GetTransactions(string searchTerm);

        Task<List<Transaction>> GetTransactionsByDate(DateTime? startDate, DateTime? endDate);
        Task<List<Transaction>> GetTransactionsByAccountHolder(string accountHolder);
        Task<List<Transaction>> GetFilteredTransactions(string? accountHolder, int? accountId, 
                                                        string? transactionType, DateTime? startDate, DateTime? endDate);
        Task AddTransaction(Transaction transaction);
        Task UpdateTransaction(Transaction transaction);
        Task DeleteTransaction(int transactionId);
    }
}
