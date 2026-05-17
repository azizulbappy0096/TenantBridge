using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class TenantAnalytics
    {
        public double RentPaid { get; set; }
        public double PendingRent { get; set; }

        public double DepositsPaid { get; set; }

        public int LeaseCount { get; set; }

        public List<Lease> LeaseExpiryAlerts { get; set; }

    }
}
