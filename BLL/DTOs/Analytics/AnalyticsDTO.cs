using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs.Analytics
{
    public class AnalyticsDTO
    {
        public int PropertyCount { get; set; }

        public int LeaseCount { get; set; }

        public List<LeaseDTO> LeaseExpiryAlerts { get; set; }

        public double RentCollected { get; set; }

        public double PendingRent { get; set; }

        public double DepositsHeld { get; set; }
    }
}
