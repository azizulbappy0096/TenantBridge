using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class AuthService
    {
        AuthRepo repo;
        Mapper mapper;


        public AuthService(AuthRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public UserDTO Get(int id)
        {
            var data = repo.Get(id);
            var entity = mapper.Map<UserDTO>(data);
            return entity;
        } 

        public UserDTO Login(string email, string password)
        {
            var data = repo.Login(email, password);
            if (data == null) return null;

            var entity = mapper.Map<UserDTO>(data);
            return entity;
        }

        public bool Register(UserDTO user)
        {
            var entity = mapper.Map<User>(user);
            return repo.Register(entity);
        }

    }
}
