using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJ_AccountManagement.Domain.Entities;
using UJ_AccountManagement.Domain.Interfaces;
using UJ_AccountManagement.Infrastructure.DBContext;

namespace UJ_AccountManagement.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public TransactionRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task AddTransaction(Transaction transaction)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            const string query = @"
                INSERT INTO Transactions (AccountId, TransactionType, ReferenceId, Amount) 
                VALUES (@AccountId, @TransactionType, @ReferenceId, @Amount);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            transaction.TransactionId = await connection.ExecuteScalarAsync<int>(query, transaction);
            connection.Close();
        }

        public async Task DeleteTransaction(int transactionId)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            const string query = "DELETE FROM Transactions WHERE TransactionId = @TransactionId";
            await connection.ExecuteAsync(query, new { TransactionId = transactionId });

            connection.Close();
        }

        public async Task<List<Transaction>> GetAllTransactions()
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var transactions = await connection.QueryAsync<Transaction>("SELECT * FROM Transactions");

            return transactions.ToList();
        }

        public async Task<List<Transaction>> GetTransactions(string searchTerm)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();

            const string query = @"
                SELECT * FROM Transactions 
                WHERE TransactionType LIKE @SearchTerm 
                OR CAST(TransactionId AS NVARCHAR) LIKE @SearchTerm
                OR CAST(AccountId AS NVARCHAR) LIKE @SearchTerm
                OR CAST(ReferenceId AS NVARCHAR) LIKE @SearchTerm
                OR CAST(Amount AS NVARCHAR) LIKE @SearchTerm";

            var transactions = await connection.QueryAsync<Transaction>(query, new { SearchTerm = $"%{searchTerm}%" });

            return transactions.ToList();
        }


        public async Task<Transaction> GetTransactionById(int transactionId)
        {
            using (var connection = await _dbConnectionFactory.CreateConnectionAsync())
            {
                var transaction = await connection.QueryFirstOrDefaultAsync<Transaction>(
                    "SELECT * FROM Transactions WHERE TransactionId = @TransactionId",
                    new { TransactionId = transactionId }
                );

                return transaction;
            }
        }


        public async Task UpdateTransaction(Transaction transaction)
        {
            var connection = await _dbConnectionFactory.CreateConnectionAsync();

            const string query = @" UPDATE Transactions
                                    SET AccountId = @AccountId,
                                        TransactionType = @TransactionType,
                                        ReferenceId = @ReferenceId,
                                        Amount = @Amount,
                                        TransactionDate = @TransactionDate
                                    WHERE TransactionId = @TransactionId";

            await connection.ExecuteAsync(query, transaction);

            connection.Close();
        }

        public async Task<List<Transaction>> GetTransactionsByDate(DateTime? startDate, DateTime? endDate)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();

            // Build the base SQL query
            var query = "SELECT * FROM Transactions WHERE 1=1";

            if (startDate.HasValue)
            {
                query += " AND TransactionDate >= @FromDate";
            }

            if (endDate.HasValue)
            {
                query += " AND TransactionDate <= @ToDate";
            }
            else if (startDate.HasValue)
            {
                query += " AND TransactionDate <= @CurrentDate";
            }

            var transactions = await connection.QueryAsync<Transaction>(query, new
            {
                FromDate = startDate?.Date, 
                ToDate = endDate?.Date, 
                CurrentDate = DateTime.UtcNow.Date
            });

            return transactions.ToList();
        }

        public async Task<List<Transaction>> GetTransactionsByAccountHolder(string accountHolder)
        {
            var connection = await _dbConnectionFactory.CreateConnectionAsync();

            const string query = @"SELECT *
                                   FROM Transactions t
                                   INNER JOIN Customers c ON t.AccountId = c.AccountId
                                   WHERE c.AccountHolder LIKE @AccountHolder";

            var transactions = await connection.QueryAsync<Transaction>(query, new { AccountHolder = "%" + accountHolder + "%" });
            return transactions.ToList();


        }

        public async Task<List<Transaction>> GetFilteredTransactions(string? accountHolder, int? accountId, 
                                                               string? transactionType, DateTime? startDate, DateTime? endDate)
        {
            var connection = await _dbConnectionFactory.CreateConnectionAsync();

            string query = @"SELECT *
                            FROM Transactions t
                            INNER JOIN Customers c ON t.AccountId = c.AccountId
                            WHERE 
                                (@AccountHolder IS NULL OR c.AccountHolder LIKE '%' + @AccountHolder + '%')
                                AND (@AccountId IS NULL OR t.AccountId = @AccountId)
                                AND (@TransactionType IS NULL OR t.TransactionType = @TransactionType)
                                AND (@StartDate IS NULL OR t.TransactionDate >= @StartDate)
                                AND (@EndDate IS NULL OR t.TransactionDate <= @EndDate)";

            var parameters = new
            {
                AccountHolder = accountHolder,
                AccountId = accountId,
                TransactionType = transactionType,
                StartDate = startDate,
                EndDate = endDate
            };

            var transactions = await connection.QueryAsync<Transaction>(query, parameters);
            return transactions.AsList();
        }
    }
}
