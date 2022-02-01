namespace Homeautomation.Service.Dtos;
public class SimpleThermostatDto
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
}

