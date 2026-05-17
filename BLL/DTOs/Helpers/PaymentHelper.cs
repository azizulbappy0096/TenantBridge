using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs.Helpers
{
    public class PaymentHelper
    {
        public static string GenerateReceiptNo(string type)
        {
            var random = Guid.NewGuid()
                .ToString("N")
                .Substring(0, 6)
                .ToUpper();
            var prefix = type switch
            {
                "Rent" => "RCPT",
                "Deposit" => "DPT",
                _ => "PAY"
            };

            return $"{prefix}-{DateTime.UtcNow:yyyyMMdd}-{random}";
        }
    }
}
