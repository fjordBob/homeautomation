using System.ComponentModel.DataAnnotations;

namespace Homeautomation.Service.Dtos;
public class SimpleThermostatDto
{
    [RegularExpression(@"^([1-9]\d*|0)$", ErrorMessage = "Characters are not allowed. Only decimal or int.")]
    public string? CurrentTemperature
    {
        get; set;
    }

    [RegularExpression(@"^([1-9]\d*|0)$", ErrorMessage = "Characters are not allowed. Only decimal or int.")]
    public string? TargetTemperature
    {
        get; set;
    }

    [RegularExpression(@"^([1-9]\d*|0)$", ErrorMessage = "Characters are not allowed. Only decimal or int.")]
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

