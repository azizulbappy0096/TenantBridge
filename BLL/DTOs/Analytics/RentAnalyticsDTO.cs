using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs.Analytics
{
    public class RentAnalyticsDTO
    {
        public double RentCollected { get; set; }

        public double PendingRent { get; set; }

        public double CollectionRate { get; set; }
    }
}
