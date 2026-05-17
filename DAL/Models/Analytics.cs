using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Analytics
    {
        public int PropertyCount { get; set; }

        public int LeaseCount { get; set; }

        public List<Lease> LeaseExpiryAlerts { get; set; }

        public double RentCollected { get; set; }

        public double PendingRent { get; set; }

        public double DepositsHeld { get; set; }
    }
}
