using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class PaymentDTO
    {
        public int Id { get; set; }

        public int PropertyId { get; set; }

        public int LeaseId { get; set; }

        public double Amount { get; set; }

        public string Type { get; set; } = null!;

        public string? RentMonth { get; set; }

        public string Status { get; set; } = null!;

        public DateTime? PaidDate { get; set; }

        public string ReceiptNo { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public virtual Lease Lease { get; set; } = null!;

        public virtual Property Property { get; set; } = null!;
    }
}
