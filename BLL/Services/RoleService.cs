using AutoMapper;
using BLL.DTOs;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class RoleService
    {
        RoleRepo repo;
        Mapper mapper;

        public RoleService(RoleRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public RoleDTO? Get(int id)
        {
            var data = this.repo.Get(id);
            return this.mapper.Map<RoleDTO>(data);
        }

        public RoleDTO? Get(string name)
        {
            var data = this.repo.Get(name);
            return this.mapper.Map<RoleDTO>(data);
        }

    }
}
