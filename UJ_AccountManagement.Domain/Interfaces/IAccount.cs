using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJ_AccountManagement.Domain.DTOs;
using UJ_AccountManagement.Domain.Entities;

namespace UJ_AccountManagement.Domain.Interfaces
{
    public interface IAccount
    {
        Task<Account> CreateAccount(Account account);
        Task<bool> UpdateAccount(Account account);
        Task<bool> DeleteAccountAsync(int accountId);


        //GET Methods
        Task<Account> GetAccountById(int accountId);
        Task<List<Account>> GetAllAccounts();
        Task<List<Account>> FilterAccounts(AccountFilter filter);
    }
}
