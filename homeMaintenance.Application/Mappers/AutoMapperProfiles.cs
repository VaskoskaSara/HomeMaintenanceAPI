using AutoMapper;
using homeMaintenance.Application.Commands.UserLogin;
using homeMaintenance.Application.Commands.UserRegistration;
using homeMaintenance.Domain.Entities;

namespace homeMaintenance.Application.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserRegistrationCommand, User>()
                .ForMember(src => src.BirthDate, dest => dest.MapFrom(src => DateTime.Parse(src.BirthDate)))
                .ReverseMap();

            CreateMap<UserLoginCommand, User>()
                .ReverseMap();
        }
    }
}
