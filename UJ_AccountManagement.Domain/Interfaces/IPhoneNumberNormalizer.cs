using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJ_AccountManagement.Domain.Interfaces
{
    public interface IPhoneNumberNormalizer
    {
        string NormalizePhoneNumber(string phoneNumber, string regionCode = "ZA");
    }
}
