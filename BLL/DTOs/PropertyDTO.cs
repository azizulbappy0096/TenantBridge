using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class PropertyDTO
    {
        public int Id { get; set; }

        public int LandlordId { get; set; }

        public string Address { get; set; } = null!;

        public bool Active { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual User Landlord { get; set; } = null!;

        public virtual ICollection<Lease> Leases { get; set; } = new List<Lease>();

        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
