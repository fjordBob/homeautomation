using AutoMapper;
using Homeautomation.Service.Dtos;
using Homeautomation.Service.Models;

namespace Homeautomation.Service.Mappers;
public class SwitchProfile : Profile
{
    public SwitchProfile()
    {
        CreateMap<SwitchDto, Switch>()
            .ForMember(
                dest => dest.IsActive,
                opt => opt.MapFrom(src => src.IsActive))
            .ForMember(
                dest => dest.TimeStamp,
                opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<Switch, SwitchDto>()
            .ForMember(
                dest => dest.IsActive,
                opt => opt.MapFrom(src => src.IsActive));

        CreateMap<Switch, SwitchOutDto>()
            .ForMember(
                dest => dest.IsActive,
                opt => opt.MapFrom(src => src.IsActive))
            .ForMember(
                dest => dest.TimeStamp,
                opt => opt.MapFrom(src => src.TimeStamp));
    }
}
