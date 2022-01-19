using AutoMapper;
using Homeautomation.Service.Dtos;
using Homeautomation.Service.Models;

namespace Homeautomation.Service.Mappers;
public class TemperatureHumidityProfile : Profile
{
    public TemperatureHumidityProfile()
    {
        CreateMap<TemperatureHumidityDto, TemperatureHumidity>()
            .ForMember(
                dest => dest.Temperature,
                opt => opt.MapFrom(src => src.Temperature))
            .ForMember(
                dest => dest.Humidity,
                opt => opt.MapFrom(src => src.Humidity))
            .ForMember(
                dest => dest.TimeStamp,
                opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<TemperatureHumidity, TemperatureHumidityOutDto>()
            .ForMember(
                dest => dest.Temperature,
                opt => opt.MapFrom(src => src.Temperature))
            .ForMember(
                dest => dest.Humidity,
                opt => opt.MapFrom(src => src.Humidity))
            .ForMember(
                dest => dest.TimeStamp,
                opt => opt.MapFrom(src => src.TimeStamp));

        CreateMap<TemperatureHumidity, TemperatureHumidityDto>()
            .ForMember(
                dest => dest.Temperature,
                opt => opt.MapFrom(src => src.Temperature))
            .ForMember(
                dest => dest.Humidity,
                opt => opt.MapFrom(src => src.Humidity));
    }
}
