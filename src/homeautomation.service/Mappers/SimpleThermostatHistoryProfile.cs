using AutoMapper;
using Homeautomation.Service.Dtos;
using Homeautomation.Service.Models;

namespace Homeautomation.Service.Mappers;
public class SimpleThermostatHistoryProfile : Profile
{
    public SimpleThermostatHistoryProfile()
    {
        CreateMap<SimpleThermostatHistoryOutDto, SimpleThermostatHistory>()
            .ForMember(
                dest => dest.DeviceId,
                opt => opt.MapFrom(src => src.DeviceId))
            .ForMember(
                dest => dest.Values,
                opt => opt.MapFrom(src => src.Values));

        CreateMap<SimpleThermostatHistory, SimpleThermostatHistoryOutDto>()
            .ForMember(
                dest => dest.DeviceId,
                opt => opt.MapFrom(src => src.DeviceId))
            .ForMember(
                dest => dest.Values,
                opt => opt.MapFrom(src => src.Values));
    }
}
