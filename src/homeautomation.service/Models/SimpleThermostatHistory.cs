namespace Homeautomation.Service.Models;
public class SimpleThermostatHistory
{
    public string? DeviceId
    {
        get; set;
    }

    public List<SimpleThermostat>? Values
    {
        get; set;
    }
}
