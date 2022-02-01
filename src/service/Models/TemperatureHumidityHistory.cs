namespace Homeautomation.Service.Models;
public class TemperatureHumidityHistory
{
    public int Id
    {
        get; set;
    }

    public string? DeviceId
    {
        get; set;
    }

    public List<TemperatureHumidity>? Values
    {
        get; set;
    }
}
