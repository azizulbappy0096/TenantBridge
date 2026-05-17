using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class RentAnalytics
    {
        public double RentCollected { get; set; }

        public double PendingRent { get; set; }

        public double CollectionRate { get; set; }
    }
}
