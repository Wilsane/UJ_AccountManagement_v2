using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJ_AccountManagement.Domain.DTOs
{
    public class TransactionDTO
    {
        public int TransactionId { get; set; }
        public long AccountId { get; set; }
        public string AccountHolder { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
        public long ReferenceId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
