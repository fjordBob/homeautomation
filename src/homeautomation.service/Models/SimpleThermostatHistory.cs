namespace Homeautomation.Service.Models;
public class SimpleThermostatHistory
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
