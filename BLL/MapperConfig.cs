using AutoMapper;
using BLL.DTOs;
using BLL.DTOs.Auth;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class MapperConfig
    {
        static MapperConfiguration config = new MapperConfiguration(opt =>
        {
            opt.CreateMap<User, UserDTO>().ReverseMap();
            opt.CreateMap<User, RegistrationDTO>().ReverseMap();
            opt.CreateMap<Role, RoleDTO>().ReverseMap();
        });

        public static Mapper GetMapper()
        {
            return new Mapper(config);
        }
    }
}
