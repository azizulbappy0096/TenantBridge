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

        public TenantAnalytics GetTenantAnalytics(int tenantId)
        {
            var paymentQuery = db.Payments
                .Where(p => p.Lease.TenantId == tenantId);
            var leaseCount = db.Leases
                .Count(l => l.TenantId == tenantId && l.Active);

            var today = DateOnly.FromDateTime(DateTime.Now);
            var next30Days = today.AddDays(30);

            var leaseExpiryAlerts = db.Leases
                .Where(l => l.TenantId == tenantId && l.EndDate >= today && l.EndDate <= next30Days && l.Active)
                .Include(l => l.Tenant)
                .ToList();

            return new TenantAnalytics
            {
                LeaseCount = leaseCount,
                LeaseExpiryAlerts = leaseExpiryAlerts,
                RentPaid = paymentQuery
                    .Where(p =>
                        p.Type == "Rent" &&
                        p.Status == "Paid")
                    .Sum(p => (double?)p.Amount) ?? 0,

                PendingRent = paymentQuery
                    .Where(p =>
                        p.Status == "Pending")
                    .Sum(p => (double?)p.Amount) ?? 0,

                DepositsPaid = paymentQuery
                    .Where(p =>
                        p.Type == "Deposit" &&
                        p.Status == "Paid")
                    .Sum(p => (double?)p.Amount) ?? 0,
            };
        }

        public RentAnalytics GetRentAnalyticsForLandlord(int landlordId)
        {
            var payments = db.Payments.Where(p => p.Property.LandlordId == landlordId && p.Type == "Rent").ToList();
            var rentCollected = payments.Where(p => p.Status == "Paid").Sum(p => (double?)p.Amount) ?? 0;
            var pendingRent = payments.Where(p => p.Status == "Pending").Sum(p => (double?)p.Amount) ?? 0;

            return new RentAnalytics
            {
                RentCollected = rentCollected,
                PendingRent = pendingRent,
                CollectionRate = rentCollected + pendingRent > 0 ? (rentCollected / (rentCollected + pendingRent)) * 100 : 0
            };
        }

        public TenantRentAnalytics GetRentAnalyticsForTenant(int tenantId)
        {
            var payments = db.Payments.Where(p => p.Lease.TenantId == tenantId && p.Type == "Rent").ToList();
            var rentPaid = payments.Where(p => p.Status == "Paid").Sum(p => (double?)p.Amount) ?? 0;
            var pendingRent = payments.Where(p => p.Status == "Pending").Sum(p => (double?)p.Amount) ?? 0;

            return new TenantRentAnalytics
            {
                RentPaid = rentPaid,
                PendingRent = pendingRent
            };
        }

        public LeaseAnalytics GetLeaseAnalyticsForLandlord(int landlordId)
        {
            var leases = db.Leases.Where(l => l.Property.LandlordId == landlordId && l.Active).ToList();
            var totalLeases = leases.Count;

            var today = DateOnly.FromDateTime(DateTime.Now);

            var ExpireIn7Days = leases.Count(l => l.EndDate <= today.AddDays(7));
            var ExpireIn30Days = leases.Count(l => l.EndDate <= today.AddDays(30));
            return new LeaseAnalytics
            {
                Total = totalLeases,
                ExpireIn7Days = ExpireIn7Days,
                ExpireIn30Days = ExpireIn30Days
            };

        }


        public LeaseAnalytics GetLeaseAnalyticsForTenant(int tenantId)
        {
            var leases = db.Leases.Where(l => l.TenantId == tenantId && l.Active).ToList();
            var totalLeases = leases.Count;

            var today = DateOnly.FromDateTime(DateTime.Now);

            var ExpireIn7Days = leases.Count(l => l.EndDate <= today.AddDays(7));
            var ExpireIn30Days = leases.Count(l => l.EndDate <= today.AddDays(30));
            return new LeaseAnalytics
            {
                Total = totalLeases,
                ExpireIn7Days = ExpireIn7Days,
                ExpireIn30Days = ExpireIn30Days
            };

        }
    }
}
