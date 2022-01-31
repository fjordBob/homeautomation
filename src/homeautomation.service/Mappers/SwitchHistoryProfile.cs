using AutoMapper;
using Homeautomation.Service.Dtos;
using Homeautomation.Service.Models;

namespace Homeautomation.Service.Mappers;
public class SwitchHistoryProfile : Profile
{
    public SwitchHistoryProfile()
    {
        CreateMap<SwitchHistoryOutDto, SwitchHistory>()
            .ForMember(
                dest => dest.DeviceId,
                opt => opt.MapFrom(src => src.DeviceId))
            .ForMember(
                dest => dest.Values,
                opt => opt.MapFrom(src => src.Values));

        CreateMap<SwitchHistory, SwitchHistoryOutDto>()
            .ForMember(
                dest => dest.DeviceId,
                opt => opt.MapFrom(src => src.DeviceId))
            .ForMember(
                dest => dest.Values,
                opt => opt.MapFrom(src => src.Values));
    }
}
