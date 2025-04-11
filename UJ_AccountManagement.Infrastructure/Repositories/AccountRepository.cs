using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJ_AccountManagement.Domain.DTOs;
using UJ_AccountManagement.Domain.Entities;
using UJ_AccountManagement.Domain.Interfaces;

namespace UJ_AccountManagement.Infrastructure.Repositories
{
    public class AccountRepository : IAccount
    {
        public async Task<Account> CreateAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAccountAsync(int accountId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Account>> FilterAccounts(AccountFilter filter)
        {
            throw new NotImplementedException();
        }

        public async Task<Account> GetAccountById(int accountId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAccount(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
