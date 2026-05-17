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
            this.mapper = MapperConfig.GetMapper();
        }

        public UserDTO Get(int id)
        {
            var data = this.repo.Get(id);
            var entity = this.mapper.Map<UserDTO>(data);
            return entity;
        }

        public List<UserDTO> GetByRole(int role)
        {
            var data = this.repo.GetByRole(role);
            var entity = this.mapper.Map<List<UserDTO>>(data);
            return entity;
        }

        public UserDTO Login(string email, string password)
        {
            var data = this.repo.Login(email, password);
            if (data == null) return null;

            var entity = this.mapper.Map<UserDTO>(data);
            return entity;
        }

        public bool Register(RegistrationDTO user)
        {
            var entity = this.mapper.Map<User>(user);
            return this.repo.Register(entity);
        }

        public bool ChangePassword(int id, string oldPassword, string newPassword)
        {
            return this.repo.ChangePassword(id, oldPassword, newPassword);
        }

        public bool Update(UpdateProfileDTO user)
        {
            var existingUser = this.repo.Get(user.Id);
            if (existingUser == null) return false;

            existingUser.FullName = user.FullName;
            existingUser.PhoneNumber = user.PhoneNumber;

            return this.repo.Update(existingUser);
        }

    }
}
