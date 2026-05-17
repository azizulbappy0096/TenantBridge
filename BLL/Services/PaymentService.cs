using AutoMapper;
using BLL.DTOs;
using BLL.DTOs.Analytics;
using BLL.DTOs.FormDTOs;
using BLL.DTOs.Helpers;
using DAL.EF.Tables;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class PaymentService
    {
        PaymentRepo repo;
        Mapper mapper;

        public PaymentService(PaymentRepo repo)
        {
            this.repo = repo;
            this.mapper = MapperConfig.GetMapper();
        }

        public PaymentDTO? Get(int id)
        {
            var data = this.repo.Get(id);
            if (data == null) return null;

            var entity = this.mapper.Map<PaymentDTO>(data);
            return entity;
        }

        public List<PaymentDTO> Get()
        {
            var data = this.repo.Get();
            var entity = this.mapper.Map<List<PaymentDTO>>(data);
            return entity;
        }


        public List<PaymentDTO> GetByLandlordId(int landlordId, string type)
        {
            var data = this.repo.GetByLandlordId(landlordId, type);
            var entity = this.mapper.Map<List<PaymentDTO>>(data);
            return entity;
        }
        public List<PaymentDTO> GetByLandlordId(int landlordId)
        {
            var data = this.repo.GetByLandlordId(landlordId);
            var entity = this.mapper.Map<List<PaymentDTO>>(data);
            return entity;
        }

        public List<PaymentDTO> GetByTenantId(int tenantId, string type)
        {
            var data = this.repo.GetByTenantId(tenantId, type);
            var entity = this.mapper.Map<List<PaymentDTO>>(data);
            return entity;
        }
        public List<PaymentDTO> GetByTenantId(int tenantId)
        {
            var data = this.repo.GetByTenantId(tenantId);
            var entity = this.mapper.Map<List<PaymentDTO>>(data);
            return entity;
        }

        public RentAnalyticsDTO GetRentAnalyticsForLandlord(int landlordId)
        {
            var data = this.repo.GetRentAnalyticsForLandlord(landlordId);
            var entity = this.mapper.Map<RentAnalyticsDTO>(data);
            return entity;
        }

        public bool Create(PaymentCreateDTO payment)
        {
            var entity = this.mapper.Map<Payment>(payment);
            entity.PropertyId = int.Parse(payment.PropertyLeaseId.Split('-')[0]);
            entity.LeaseId = int.Parse(payment.PropertyLeaseId.Split('-')[1]);
            entity.ReceiptNo = PaymentHelper.GenerateReceiptNo(payment.Type);
            return this.repo.Create(entity);
        }

        public bool Update(int id, string Status)
        {
            var entity = this.repo.Get(id);
            if (entity == null) return false;

            entity.Status = Status;
            return this.repo.Update(entity);
        }

        public bool Delete(int id)
        {
            return this.repo.Delete(id);
        }
    }
}
