using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJ_AccountManagement.Domain.Entities;

namespace UJ_AccountManagement.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customers>> GetAllCustomers();
    }
}
