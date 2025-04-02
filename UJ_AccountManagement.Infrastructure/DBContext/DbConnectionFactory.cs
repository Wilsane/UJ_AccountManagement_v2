using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJ_AccountManagement.Infrastructure.DBContext
{
    public class DbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public DbConnectionFactory(IConfiguration configuration) =>_configuration = configuration;


        public async Task<IDbConnection> CreateConnectionAsync()
        {
            try
            {
                var connection = new SqlConnection(_configuration.GetConnectionString("MSSQLConString"));
                await connection.OpenAsync();
                return connection;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating database connection:\t", ex);
            }
        }


    }
}
