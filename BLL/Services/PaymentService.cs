using AutoMapper;
using BLL.DTOs;
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

        public bool Create(PaymentDTO payment)
        {
            var entity = this.mapper.Map<Payment>(payment);
            return this.repo.Create(entity);
        }

        public bool Delete(int id)
        {
            return this.repo.Delete(id);
        }
    }
}
