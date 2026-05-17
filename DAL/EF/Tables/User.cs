using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<Lease> LeaseLandlords { get; set; } = new List<Lease>();

    public virtual ICollection<Lease> LeaseTenants { get; set; } = new List<Lease>();

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();

    public virtual ICollection<Reminder> ReminderReceivedByNavigations { get; set; } = new List<Reminder>();

    public virtual ICollection<Reminder> ReminderSentByNavigations { get; set; } = new List<Reminder>();

    public virtual Role RoleNavigation { get; set; } = null!;
}
