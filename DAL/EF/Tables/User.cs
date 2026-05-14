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

    public virtual Role RoleNavigation { get; set; } = null!;
}
