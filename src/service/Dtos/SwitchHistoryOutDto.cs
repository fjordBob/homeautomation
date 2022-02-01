namespace Homeautomation.Service.Dtos;
public class SwitchHistoryOutDto
{
    public string? DeviceId
    {
        get; set;
    }

    public List<SwitchOutDto>? Values
    {
        get; set;
    }
}