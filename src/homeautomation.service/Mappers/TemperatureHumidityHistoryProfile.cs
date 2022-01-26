using AutoMapper;
using Homeautomation.Service.Dtos;
using Homeautomation.Service.Models;

namespace Homeautomation.Service.Mappers;
public class TemperatureHumidityHistoryProfile : Profile
{
    public TemperatureHumidityHistoryProfile()
    {
        CreateMap<TemperatureHumidityHistoryOutDto, TemperatureHumidityHistory>()
            .ForMember(
                dest => dest.DeviceId,
                opt => opt.MapFrom(src => src.DeviceId))
            .ForMember(
                dest => dest.Values,
                opt => opt.MapFrom(src => src.TemperatureHumidityList));

        CreateMap<TemperatureHumidityHistory, TemperatureHumidityHistoryOutDto>()
            .ForMember(
                dest => dest.DeviceId,
                opt => opt.MapFrom(src => src.DeviceId))
            .ForMember(
                dest => dest.TemperatureHumidityList,
                opt => opt.MapFrom(src => src.Values));
    }
}
