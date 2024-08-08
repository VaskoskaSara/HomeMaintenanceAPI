﻿using Amazon.Runtime;
using AutoMapper;
using homeMaintenance.Application.Commands.UserLogin;
using homeMaintenance.Application.Commands.UserRegistration;
using homeMaintenance.Domain.Entities;
using System.Globalization;

namespace homeMaintenance.Application.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserRegistrationCommand, User>()
           .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => ParseDate(src.BirthDate)))
           .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar.FileName))
            .ForMember(dest => dest.PositionId, opt => opt.MapFrom(src => ConvertToGuid(src.PositionId)))
            .ReverseMap()
           .ForMember(dest => dest.Photos, opt => opt.Ignore());

            CreateMap<UserLoginCommand, User>()
                .ReverseMap();


            CreateMap<User, EmployeeDto>()
            .ReverseMap();

            CreateMap<UserLoginCommand, User>()
                .ReverseMap();
        }

        private DateTime? ParseDate(string dateString)
        {
            string format = "ddd, dd MMM yyyy HH:mm:ss 'GMT'";

            DateTimeOffset.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset dateTimeOffset);
            
            DateTime dateTime = dateTimeOffset.DateTime;

            Console.WriteLine("DateTime: " + dateTime); // Output: 21/03/1998 00:00:00

            return dateTime;
        }

        private Guid? ConvertToGuid(string positionId)
        {
            if (Guid.TryParse(positionId, out Guid parsedGuid))
            {
                return parsedGuid;
            }
            return null;
        }
    }

}
