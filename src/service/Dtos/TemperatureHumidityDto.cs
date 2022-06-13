using System.ComponentModel.DataAnnotations;

namespace Homeautomation.Service.Dtos;
public class TemperatureHumidityDto
{
    [RegularExpression(@"^([1-9]\d*|0)$", ErrorMessage = "Characters are not allowed. Only decimal or int.")]
    public string? Temperature
    {
        get; set;
    }

    [RegularExpression(@"^([1-9]\d*|0)$", ErrorMessage = "Characters are not allowed. Only decimal or int.")]
    public string? Humidity
    {
        get; set;
    }
}
