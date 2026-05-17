using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Lease
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public int LandlordId { get; set; }

    public int TenantId { get; set; }

    public double RentAmount { get; set; }

    public double DepositAmount { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool Active { get; set; }

    public virtual User Landlord { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Property Property { get; set; } = null!;

    public virtual User Tenant { get; set; } = null!;
}
