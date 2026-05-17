using AutoMapper;
using BLL.DTOs;
using BLL.DTOs.Analytics;
using BLL.DTOs.Auth;
using BLL.DTOs.FormDTOs;
using DAL.EF.Tables;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class MapperConfig
    {
        static MapperConfiguration config = new MapperConfiguration(opt =>
        {
            // User
            opt.CreateMap<User, UserDTO>().ReverseMap();

            // Auth
            opt.CreateMap<User, RegistrationDTO>().ReverseMap();
            opt.CreateMap<User, UpdateProfileDTO>().ReverseMap();

            // Role
            opt.CreateMap<Role, RoleDTO>().ReverseMap();

            // Property
            opt.CreateMap<Property, PropertyDTO>().ReverseMap();

            // Lease
            opt.CreateMap<Lease, LeaseDTO>().ReverseMap();
            opt.CreateMap<Lease, LeaseCreateDTO>().ReverseMap();

            // Payment
            opt.CreateMap<Payment, PaymentDTO>().ReverseMap();
            opt.CreateMap<Payment, PaymentCreateDTO>().ReverseMap();

            // Analytics
            opt.CreateMap<Analytics, AnalyticsDTO>().ReverseMap();
            opt.CreateMap<RentAnalytics, RentAnalyticsDTO>().ReverseMap();
            opt.CreateMap<LeaseAnalytics, LeaseAnalyticsDTO>().ReverseMap();
        });

        public static Mapper GetMapper()
        {
            return new Mapper(config);
        }
    }
}
