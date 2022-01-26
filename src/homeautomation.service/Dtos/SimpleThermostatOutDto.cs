namespace Homeautomation.Service.Dtos;
public class SimpleThermostatOutDto
{
    public string? CurrentTemperature
    {
        get; set;
    }

    public string? TargetTemperature
    {
        get; set;
    }

    public string? HeatingThresholdTemperature
    {
        get; set;
    }

    public string? Status
    {
        get; set;
    }

    public string? HeatingStatus
    {
        get; set;
    }

    public DateTime? TimeStamp
    {
        get; set;
    }
}
