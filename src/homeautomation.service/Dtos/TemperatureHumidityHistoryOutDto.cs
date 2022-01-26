namespace Homeautomation.Service.Dtos;
public class TemperatureHumidityHistoryOutDto
{
    public string? DeviceId
    {
        get; set;
    }

    public List<TemperatureHumidityOutDto>? Values
    {
        get; set;
    }
}
