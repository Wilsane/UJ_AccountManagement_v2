using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneNumbers;
using UJ_AccountManagement.Domain.Interfaces;

namespace UJ_AccountManagement.Services
{
    public class PhoneNumberNormalizer : IPhoneNumberNormalizer
    {
        private readonly PhoneNumberUtil _phoneNumberUtil;
        public string NormalizePhoneNumber(string phoneNumber, string regionCode)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber), "Phone number cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(regionCode))
                throw new ArgumentNullException(nameof(regionCode), "Region code cannot be null or empty.");

            try
            {
                var phoneUtil = PhoneNumbers.PhoneNumberUtil.GetInstance(); // libphonenumber-csharp
                var parsedNumber = phoneUtil.Parse(phoneNumber, regionCode);
                return phoneUtil.Format(parsedNumber, PhoneNumbers.PhoneNumberFormat.E164);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to normalize phone number: {phoneNumber}", ex);
            }
        }


    }
}
