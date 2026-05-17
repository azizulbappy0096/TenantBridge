using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs.Analytics
{
    public class TenantAnalyticsDTO
    {
        public double RentPaid { get; set; }
        public double PendingRent { get; set; }

        public double DepositsPaid { get; set; }

        public int LeaseCount { get; set; }

        public List<LeaseDTO> LeaseExpiryAlerts { get; set; }
    }
}
