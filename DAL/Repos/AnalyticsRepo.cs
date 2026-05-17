using DAL.EF;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class AnalyticsRepo
    {
        TenantBridgeContext db;

        public AnalyticsRepo(TenantBridgeContext db)
        {
            this.db = db;
        }

        public Analytics GetLandlordAnalytics(int landlordId)
        {
            var paymentQuery = db.Payments
                .Where(p => p.Property.LandlordId == landlordId);
            var propertyCount = db.Properties
                .Count(p => p.LandlordId == landlordId && p.Active);
            var leaseCount = db.Leases
                .Count(l => l.Property.LandlordId == landlordId && l.Active);

            var today = DateOnly.FromDateTime(DateTime.Now);
            var next30Days = today.AddDays(30);

            var leaseExpiryAlerts = db.Leases
                .Where(l => l.Property.LandlordId == landlordId && l.EndDate >= today && l.EndDate <= next30Days && l.Active)
                .Include(l => l.Tenant)
                .ToList();

            return new Analytics
            {
                PropertyCount = propertyCount,
                LeaseCount = leaseCount,
                LeaseExpiryAlerts = leaseExpiryAlerts,
                RentCollected = paymentQuery
                    .Where(p =>
                        p.Type == "Rent" &&
                        p.Status == "Paid")
                    .Sum(p => (double?)p.Amount) ?? 0,

                PendingRent = paymentQuery
                    .Where(p =>
                        p.Status == "Pending")
                    .Sum(p => (double?)p.Amount) ?? 0,

                DepositsHeld = paymentQuery
                    .Where(p =>
                        p.Type == "Deposit" &&
                        p.Status == "Paid")
                    .Sum(p => (double?)p.Amount) ?? 0,
            };
        }
    }
}
