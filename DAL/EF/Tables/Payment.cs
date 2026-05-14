using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Payment
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public int TenantId { get; set; }

    public double Amount { get; set; }

    public string Type { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime PaidDate { get; set; }

    public string ReceiptNo { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Property Property { get; set; } = null!;

    public virtual User Tenant { get; set; } = null!;
}
