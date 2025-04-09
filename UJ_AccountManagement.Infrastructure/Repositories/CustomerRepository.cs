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
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public CustomerRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<List<Customers>> GetAllCustomers()
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            string query = "SELECT * FROM Customers";

            var customers = await connection.QueryAsync<Customers>(query);

            return customers.ToList();

        }
    }
}
