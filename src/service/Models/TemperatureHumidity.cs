namespace Homeautomation.Service.Models;
public class TemperatureHumidity
{
    public int Id
    {
        get; set;
    }

    public string? Temperature
    {
        get; set;
    }

    public string? Humidity
    {
        get; set;
    }

    public DateTime? TimeStamp
    {
        get; set;
    }
}
