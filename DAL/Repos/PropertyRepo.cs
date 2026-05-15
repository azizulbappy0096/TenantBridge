using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class PropertyRepo
    {
        TenantBridgeContext db;
        LeaseRepo leaseRepo;

        public PropertyRepo(TenantBridgeContext db, LeaseRepo leaseRepo)
        {
            this.db = db;
            this.leaseRepo = leaseRepo;
        }

        public Property? Get(int id)
        {
            return db.Properties.Where(p => p.Id == id).FirstOrDefault();
        }

        public List<Property> Get()
        {
            return db.Properties.ToList();
        }

        public List<Property> GetByLandlord(int landlordId)
        {
            return db.Properties.Where(p => p.LandlordId == landlordId).OrderByDescending(p => p.CreatedAt)
                    .Include(p => p.Landlord)
                    .Include(p => p.Leases.Where(l => l.Active)
                                          .OrderByDescending(l => l.CreatedAt)
                                          .Take(1))
                    .ThenInclude(l => l.Tenant)
                    .ToList();
        }

        public bool Create(Property property)
        {
            db.Properties.Add(property);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var property = Get(id);
            if (property == null) return false;
            property.Active = false;
            leaseRepo.DeleteByProperty(id);

            return db.SaveChanges() > 0;
        }

    }
}
