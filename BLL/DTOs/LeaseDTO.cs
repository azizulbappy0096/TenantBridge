using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class LeaseDTO
    {
        public int Id { get; set; }

        public int PropertyId { get; set; }

        public int TenantId { get; set; }

        public double RentAmount { get; set; }

        public double DepositAmount { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Property Property { get; set; } = null!;

        public virtual User Tenant { get; set; } = null!;

        public bool Active { get; set; }
    }
}
