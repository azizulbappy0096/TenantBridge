using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class LeaseRepo
    {
        TenantBridgeContext db;

        public LeaseRepo(TenantBridgeContext db)
        {
            this.db = db;
        }

        public Lease? Get(int id)
        {
            return db.Leases.Where(l => l.Id == id).FirstOrDefault();
        }

        public List<Lease> GetByTenantId(int tenantId)
        {
            return db.Leases.Where(l => l.Active && l.TenantId == tenantId)
                .Include(l => l.Property)
                .Include(l => l.Landlord)
                .OrderByDescending(l => l.StartDate)
                .ToList();
        }

        public List<Lease> GetByLandlordId(int landlordId)
        {
            return db.Leases.Where(l => l.Active && l.LandlordId == landlordId)
                .Include(l => l.Property)
                .Include(l => l.Tenant)
                .Include(l => l.Landlord)
                .OrderByDescending(l => l.StartDate)
                .ToList();
        }

        public bool Create(Lease lease)
        {
            lease.Active = true;
            db.Leases.Add(lease);
            return db.SaveChanges() > 0;
        }

        public bool Update(int id, Lease lease)
        {
            var data = Get(id);
            if (data == null)
                return false;
            data.Active = false;

            lease.Active = true;
            db.Leases.Add(lease);

            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var lease = Get(id);
            if (lease == null) return false;
            lease.Active = false;
            return db.SaveChanges() > 0;
        }

        public bool DeleteByProperty(int propertyId)
        {
            var leases = db.Leases.Where(l => l.PropertyId == propertyId).ToList();
            leases.ForEach(l => l.Active = false);
            return db.SaveChanges() > 0;
        }
    }
}
