﻿using AutoMapper;
using homeMaintenance.Application.Commands.EmployeeDisabledDates;
using homeMaintenance.Application.Commands.EmployeeReview;
using homeMaintenance.Application.Commands.TransactionInformation;
using homeMaintenance.Application.Commands.UserLogIn;
using homeMaintenance.Application.Commands.UserRegistration;
using homeMaintenance.Application.DTOs;
using homeMaintenance.Domain.Entities;
using homeMaintenance.Domain.Enum;
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
           .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.PaymentType == PaymentType.excludeByContract ? null : src.Price))
            .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos.Select(file => file.FileName).ToList()))
            .ReverseMap();

            CreateMap<UserLoginCommand, User>()
                .ReverseMap();


            CreateMap<User, EmployeeDto>()
            .ReverseMap();

            CreateMap<UserLoginCommand, User>()
                .ReverseMap();

            CreateMap<TransactionInfoCommand, TransactionInfo>()
            .ReverseMap();

            CreateMap<BookingInfo, BookingInfoDto>()
            .ReverseMap();

            CreateMap<EmployeeDisabledDates, DisabledDatesByEmployee>()
           .ReverseMap();

            CreateMap<EmployeeReviewCommand, AddUserReview>()
            .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos.Select(file => file.FileName).ToList()))
           .ReverseMap();

            CreateMap<Position, PositionDto>().ReverseMap();
        }

        private DateTime? ParseDate(string dateString)
        {
            string format = "ddd, dd MMM yyyy HH:mm:ss 'GMT'";

            DateTimeOffset.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset dateTimeOffset);
            
            DateTime dateTime = dateTimeOffset.DateTime;

            Console.WriteLine("DateTime: " + dateTime);

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
