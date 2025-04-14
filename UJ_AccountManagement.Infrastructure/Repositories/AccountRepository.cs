using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using UJ_AccountManagement.Domain.DTOs;
using UJ_AccountManagement.Domain.Entities;
using UJ_AccountManagement.Domain.Interfaces;
using UJ_AccountManagement.Infrastructure.DBContext;

namespace UJ_AccountManagement.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DbConnectionFactory _dbConnectionFactory;
        private readonly IPhoneNumberNormalizer _phoneNumberNormalizer;

        public AccountRepository(DbConnectionFactory dbConnectionFactory,
            IPhoneNumberNormalizer phoneNumberNormalizer)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _phoneNumberNormalizer = phoneNumberNormalizer;
        }

        public async Task CreateAccount(Account account)
        {

            using var connection = await _dbConnectionFactory.CreateConnectionAsync();

            const string query = @"
                INSERT INTO Account (AccountHolder, Phone1, Phone2, Email, AccountLimit, Balance) 
                VALUES (@AccountHolder, @Phone1, @Phone2, @Email, @AccountLimit, @Balance);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            account.AccountId = await connection.ExecuteScalarAsync<int>(query, account);
            connection.Close();
        }


        public async Task DeleteAccount(int accountId)
        {
            using var connection=await _dbConnectionFactory.CreateConnectionAsync();
            const string query = "DELETE FROM Account WHERE AccountId = @AccountId";
            await connection.ExecuteAsync(query, new { AccountId = accountId });
            connection.Close();
        }

        public async Task<Account> GetAccountById(int accountId)
        {
            using (var connection = await _dbConnectionFactory.CreateConnectionAsync())
            {
                var accountResult = await connection.QueryFirstOrDefaultAsync<Account>(
                    "SELECT * FROM Account WHERE AccountId = @AccountId",
                    new { accountId = accountId }
                );

                return accountResult;
            }
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            const string query = "SELECT * FROM Account";
            var accounts = await connection.QueryAsync<Account>(query);

            return accounts.AsList();
        }

        public async Task UpdateAccount(Account account)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            const string query = @"
                UPDATE Account 
                SET AccountHolder = @AccountHolder, 
                    Phone1 = @Phone1, 
                    Phone2 = @Phone2, 
                    Email = @Email, 
                    AccountLimit = @AccountLimit, 
                    Balance = @Balance
                WHERE AccountId = @AccountId";
            await connection.ExecuteAsync(query, account);
            connection.Close();
        }

        public async Task<List<Account>> FilterAccounts(string? accountHolder, string? email, string? phone, decimal? minBalance, decimal? maxBalance, DateTime? createdFrom, DateTime? createdTo)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();

            var query = @"
        SELECT *
        FROM Account
        WHERE
            (@AccountHolder IS NULL OR AccountHolder LIKE '%' + @AccountHolder + '%')
            AND (@Email IS NULL OR Email LIKE '%' + @Email + '%')
            AND (@Phone IS NULL OR Phone1 LIKE '%' + @Phone + '%' OR Phone2 LIKE '%' + @Phone + '%')
            AND (@MinBalance IS NULL OR Balance >= @MinBalance)
            AND (@MaxBalance IS NULL OR Balance <= @MaxBalance)
            AND (@CreatedFrom IS NULL OR CreatedDate >= @CreatedFrom)
            AND (@CreatedTo IS NULL OR CreatedDate <= @CreatedTo)
    ";

            var parameters = new
            {
                AccountHolder = accountHolder,
                Email = email,
                Phone = phone,
                MinBalance = minBalance,
                MaxBalance = maxBalance,
                CreatedFrom = createdFrom,
                CreatedTo = createdTo
            };

            var accounts = await connection.QueryAsync<Account>(query, parameters);
            return accounts.AsList();
        }


    }
}
