using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJ_AccountManagement.Domain.Entities
{
    public class Transaction
    {
        [Key]
        public  int TransactionId { get; set; }
        public required long AccountId { get; set; }
        public required string TransactionType { get; set; }=string.Empty;
        public  long ReferenceId { get; set; }
        public required decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
