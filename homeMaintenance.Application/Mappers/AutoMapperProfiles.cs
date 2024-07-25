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
            //CreateMap<UserRegistrationCommand, User>()
            //    .ForMember(src => src.BirthDate, dest => dest.MapFrom(src => DateTime.Parse(src.BirthDate)))
            //    .ReverseMap();

            //CreateMap<User, UserRegistrationCommand>()
            //   .ForMember(src => src.Avatar, dest => dest.Ignore())
            //   .ForMember(src => src.Photos, dest => dest.Ignore())
            //   .ReverseMap();

            CreateMap<UserRegistrationCommand, User>()
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => ParseCustomDate(src.BirthDate)))
           .ReverseMap()
           .ForMember(dest => dest.Avatar, opt => opt.Ignore())
           .ForMember(dest => dest.Photos, opt => opt.Ignore());

            CreateMap<UserLoginCommand, User>()
                .ReverseMap();
        }

        private DateTime ParseCustomDate(string date)
        {
            return DateTime.ParseExact(date, "ddd MMM dd yyyy HH:mm:ss 'GMT'K '(Central European Standard Time)'", CultureInfo.InvariantCulture);
        }
    }

}
