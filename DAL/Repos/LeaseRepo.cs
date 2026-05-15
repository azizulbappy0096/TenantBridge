using DAL.EF;
using DAL.EF.Tables;
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

        public List<Lease> Get()
        {
            return db.Leases.ToList();
        }

        public bool Create(Lease lease)
        {
            db.Leases.Add(lease);
            return db.SaveChanges() > 0;
        }

        public bool Update(Lease lease)
        {
            var data = Get(lease.Id);
            if (data == null)
                return false;
            data.Active = false;

            lease.Id = 0;
            db.Leases.Add(lease);

            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var lease = Get(id);
            if (lease == null) return false;
            db.Leases.Remove(lease);
            return db.SaveChanges() > 0;
        }
    }
}
