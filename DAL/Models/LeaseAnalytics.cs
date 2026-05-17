using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class LeaseAnalytics
    {
        public int Total { get; set; }
        public int ExpireIn7Days { get; set; }
        public int ExpireIn30Days { get; set; }
    }
}
