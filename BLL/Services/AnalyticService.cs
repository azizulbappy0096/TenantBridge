using AutoMapper;
using BLL.DTOs.Analytics;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class AnalyticService
    {
        AnalyticsRepo repo;
        Mapper mapper;

        public AnalyticService(AnalyticsRepo repo)
        {
            this.repo = repo;
            this.mapper = MapperConfig.GetMapper();
        }

        public AnalyticsDTO GetLandlordAnalytics(int landlordId)
        {
            var analytics = this.repo.GetLandlordAnalytics(landlordId);
            return this.mapper.Map<AnalyticsDTO>(analytics);
        }

        public RentAnalyticsDTO GetRentAnalyticsForLandlord(int landlordId)
        {
            var data = this.repo.GetRentAnalyticsForLandlord(landlordId);
            var entity = this.mapper.Map<RentAnalyticsDTO>(data);
            return entity;
        }

        public LeaseAnalyticsDTO GetLeaseAnalyticsForLandlord(int landlordId)
        {
            var data = this.repo.GetLeaseAnalyticsForLandlord(landlordId);
            var entity = this.mapper.Map<LeaseAnalyticsDTO>(data);
            return entity;
        }


    }
}
