using DAL.EF;
using DAL.EF.Tables;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
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

        public List<Payment> GetByLandlordId(int landlordId)
        {
            return db.Payments.Include(p => p.Property).Include(p => p.Lease).Where(p => p.Property.LandlordId == landlordId).OrderByDescending(p => p.PaidDate).ToList();
        }
        public List<Payment> GetByLandlordId(int landlordId, string type)
        {
            return db.Payments.Include(p => p.Property).Include(p => p.Lease).Include(p => p.Lease.Tenant).Where(p => p.Property.LandlordId == landlordId && p.Type == type).OrderByDescending(p => p.PaidDate).ToList();
        }


        public List<Payment> GetByTenantId(int tenantId)
        {
            return db.Payments.Include(p => p.Property).Include(p => p.Lease).Where(p => p.Lease.TenantId == tenantId).OrderByDescending(p => p.PaidDate).ToList();
        }
        public List<Payment> GetByTenantId(int tenantId, string type)
        {
            return db.Payments.Include(p => p.Property).Include(p => p.Lease).Where(p => p.Lease.TenantId == tenantId && p.Type == type).OrderByDescending(p => p.PaidDate).ToList();
        }


       


        public bool Create(Payment payment)
        {
            db.Payments.Add(payment);
            return db.SaveChanges() > 0;
        }

        public bool Update(Payment payment)
        {
            db.Entry(payment).CurrentValues.SetValues(payment);
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
