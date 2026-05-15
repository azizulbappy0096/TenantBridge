using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class PropertyRepo
    {
        TenantBridgeContext db;

        public PropertyRepo(TenantBridgeContext db)
        {
            this.db = db;
        }

        public Property? Get(int id)
        {
            return db.Properties.Where(p => p.Id == id).FirstOrDefault();
        }

        public List<Property> Get()
        {
            return db.Properties.ToList();
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
            db.Properties.Remove(property);
            return db.SaveChanges() > 0;
        }

    }
}
