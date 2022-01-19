namespace Homeautomation.Service.Models;
public class TemperatureHumidityHistory
{
    public string? Id
    {
        get; set;
    }

    public List<TemperatureHumidity>? TemperatureHumidityList
    {
        get; set;
    }
}
