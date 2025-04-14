using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJ_AccountManagement.Domain.DTOs;
using UJ_AccountManagement.Domain.Entities;

namespace UJ_AccountManagement.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task CreateAccount(Account account);
        Task UpdateAccount(Account account);
        Task DeleteAccount(int accountId);


        //GET Methods
        Task<Account> GetAccountById(int accountId);
        Task<List<Account>> GetAllAccounts();
        Task<List<Account>> FilterAccounts(string? accountHolder, string? email, string? phone,
            decimal? minBalance, decimal? maxBalance, DateTime? createdFrom, DateTime? createdTo);
    }
}
