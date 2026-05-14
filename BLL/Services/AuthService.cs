using AutoMapper;
using BLL.DTOs;
using BLL.DTOs.Auth;
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

        public bool Register(RegistrationDTO user)
        {
            var entity = mapper.Map<User>(user);
            return repo.Register(entity);
        }

        public bool ChangePassword(int id, string oldPassword, string newPassword)
        {
            return repo.ChangePassword(id, oldPassword, newPassword);
        }

        public bool Update(UpdateProfileDTO user)
        {
            var existingUser = repo.Get(user.Id);
            if (existingUser == null) return false;

            existingUser.FullName = user.FullName;
            existingUser.PhoneNumber = user.PhoneNumber;

            return repo.Update(existingUser);
        }

    }
}
