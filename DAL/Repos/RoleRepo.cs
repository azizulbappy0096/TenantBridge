using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class RoleRepo
    {
        TenantBridgeContext db;

        public RoleRepo(TenantBridgeContext db)
        {
            this.db = db;
        }

        public Role? Get(int id)
        {
            return db.Roles.Where(r => r.Id == id).FirstOrDefault();
        }

        public Role? Get(string name)
        {
            return db.Roles.Where(r => r.Name == name).FirstOrDefault();
        }


    }
}
