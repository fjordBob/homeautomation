namespace Homeautomation.Service.Models;
public class SimpleThermostatHistory
{
    public int Id
    {
        get; set;
    }

    public string? DeviceId
    {
        get; set;
    }

    public List<SimpleThermostat>? Values
    {
        get; set;
    }
}
