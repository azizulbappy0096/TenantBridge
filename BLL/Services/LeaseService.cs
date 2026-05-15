using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class LeaseService
    {
        LeaseRepo repo;
        Mapper mapper;

        public LeaseService(LeaseRepo repo)
        {
            this.repo = repo;
            this.mapper = MapperConfig.GetMapper();
        }

        public LeaseDTO? Get(int id)
        {
            var data = this.repo.Get(id);
            if(data == null) return null;

            var entity = this.mapper.Map<LeaseDTO>(data);
            return entity;
        }

        public List<LeaseDTO> Get()
        {
            var data = this.repo.Get();
            var entity = this.mapper.Map<List<LeaseDTO>>(data);
            return entity;
        }

        public bool Create(LeaseDTO lease)
        {
            var entity = this.mapper.Map<Lease>(lease);
            return this.repo.Create(entity);
        }

        public bool Update(LeaseDTO lease)
        {
            var entity = this.mapper.Map<Lease>(lease);
            return this.repo.Update(entity);
        }

        public bool Delete(int id)
        {
            return this.repo.Delete(id);
        }


    }
}
