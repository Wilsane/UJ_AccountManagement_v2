using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJ_AccountManagement.Domain.Entities
{
    public class Account
    {
        public int AccountId { get; set; }
        public string? AccountHolder { get; set; }
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? Email { get; set; }
        public decimal? AccountLimit { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
