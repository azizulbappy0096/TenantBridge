using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Payment
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public int LeaseId { get; set; }

    public double Amount { get; set; }

    public string Type { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? RentMonth { get; set; }

    public DateTime? PaidDate { get; set; }

    public string ReceiptNo { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Lease Lease { get; set; } = null!;

    public virtual Property Property { get; set; } = null!;
}
