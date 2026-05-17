using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs.Analytics
{
    public class LeaseAnalyticsDTO
    {
        public int Total { get; set; }
        public int ExpireIn7Days { get; set; }
        public int ExpireIn30Days { get; set; }
    }
}
