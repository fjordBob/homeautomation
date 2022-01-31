using AutoMapper;
using Homeautomation.Service.Dtos;
using Homeautomation.Service.Models;

namespace Homeautomation.Service.Mappers;

public class SimpleThermostatProfile : Profile
{
    public SimpleThermostatProfile()
    {
        CreateMap<SimpleThermostatDto, SimpleThermostat>()
            .ForMember(
                dest => dest.CurrentTemperature,
                opt => opt.MapFrom(src => src.CurrentTemperature))
            .ForMember(
                dest => dest.TargetTemperature,
                opt => opt.MapFrom(src => src.TargetTemperature))
            .ForMember(
                dest => dest.HeatingThresholdTemperature,
                opt => opt.MapFrom(src => src.HeatingThresholdTemperature))
            .ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(src => src.Status))
            .ForMember(
                dest => dest.HeatingStatus,
                opt => opt.MapFrom(src => src.HeatingStatus))
            .ForMember(
                dest => dest.TimeStamp,
                opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<SimpleThermostat, SimpleThermostatDto>()
            .ForMember(
                dest => dest.CurrentTemperature,
                opt => opt.MapFrom(src => src.CurrentTemperature))
            .ForMember(
                dest => dest.TargetTemperature,
                opt => opt.MapFrom(src => src.TargetTemperature))
            .ForMember(
                dest => dest.HeatingThresholdTemperature,
                opt => opt.MapFrom(src => src.HeatingThresholdTemperature))
            .ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(src => src.Status))
            .ForMember(
                dest => dest.HeatingStatus,
                opt => opt.MapFrom(src => src.HeatingStatus));

        CreateMap<SimpleThermostat, SimpleThermostatOutDto>()
            .ForMember(
                dest => dest.CurrentTemperature,
                opt => opt.MapFrom(src => src.CurrentTemperature))
            .ForMember(
                dest => dest.TargetTemperature,
                opt => opt.MapFrom(src => src.TargetTemperature))
            .ForMember(
                dest => dest.HeatingThresholdTemperature,
                opt => opt.MapFrom(src => src.HeatingThresholdTemperature))
            .ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(src => src.Status))
            .ForMember(
                dest => dest.HeatingStatus,
                opt => opt.MapFrom(src => src.HeatingStatus))
            .ForMember(
                dest => dest.TimeStamp,
                opt => opt.MapFrom(src => src.TimeStamp));
    }
}