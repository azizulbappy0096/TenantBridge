using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class PropertyService
    {
        PropertyRepo repo;
        Mapper mapper;

        public PropertyService(PropertyRepo repo)
        {
            this.repo = repo;
            this.mapper = MapperConfig.GetMapper();
        }

        public PropertyDTO? Get(int id)
        {
            var data = this.repo.Get(id);
            if (data == null) return null;

            var entity = this.mapper.Map<PropertyDTO>(data);
            return entity;
        }

        public List<PropertyDTO> Get()
        {
            var data = this.repo.Get();
            var entity = this.mapper.Map<List<PropertyDTO>>(data);
            return entity;
        }

        public bool Create(PropertyDTO property)
        {
            var entity = this.mapper.Map<Property>(property);
            return this.repo.Create(entity);
        }

        public bool Delete(int id)
        {
            return this.repo.Delete(id);
        }
    }
}
