using Homeautomation.Service.Models;

namespace Homeautomation.Service.Dtos;
public class SimpleThermostatHistoryOutDto
{
    public string? DeviceId
    {
        get; set;
    }

    public List<SimpleThermostatHistory>? Values
    {
        get; set;
    }
}
