namespace Homeautomation.Service.Dtos;

public class TemperatureHumidityOutDto
{
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
