namespace Homeautomation.Service.Dtos;
public class TemperatureHumidityHistoryOutDto
{
    public string? Id
    {
        get; set;
    }

    public List<TemperatureHumidityOutDto>? TemperatureHumidityList
    {
        get; set;
    }
}
