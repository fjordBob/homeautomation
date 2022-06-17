namespace Frontend.Dtos;

public class TemperatureHumidityDto
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
