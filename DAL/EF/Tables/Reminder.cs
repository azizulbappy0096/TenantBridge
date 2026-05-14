using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Reminder
{
    public int Id { get; set; }

    public int ReceivedBy { get; set; }

    public int SentBy { get; set; }

    public string Message { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual User ReceivedByNavigation { get; set; } = null!;

    public virtual User SentByNavigation { get; set; } = null!;
}
