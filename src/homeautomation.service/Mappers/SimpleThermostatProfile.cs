using AutoMapper;
using Homeautomation.Service.Dtos;
using Homeautomation.Service.Models;

namespace Homeautomation.Service.Mappers
{
    public class SimpleThermostatProfile : Profile
    {
        public SimpleThermostatProfile()
        {
            CreateMap<SimpleThermostatHistoryOutDto, SimpleThermostatHistory>()
                .ForMember(
                    dest => dest.DeviceId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.Values,
                    opt => opt.MapFrom(src => src.TemperatureHumidityList));

            CreateMap<SimpleThermostatHistory, SimpleThermostatHistoryOutDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.DeviceId))
                .ForMember(
                    dest => dest.TemperatureHumidityList,
                    opt => opt.MapFrom(src => src.Values));
        }
    }
}
