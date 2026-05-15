using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class PaymentRepo
    {
        TenantBridgeContext db;

        public PaymentRepo(TenantBridgeContext db)
        {
            this.db = db;
        }

        public Payment? Get(int id)
        {
            return db.Payments.Where(p => p.Id == id).FirstOrDefault();
        }

        public List<Payment> Get()
        {
            return db.Payments.ToList();
        }

        public bool Create(Payment payment)
        {
            db.Payments.Add(payment);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var payment = Get(id);
            if (payment == null) return false;
            db.Payments.Remove(payment);
            return db.SaveChanges() > 0;
        }


    }
}
