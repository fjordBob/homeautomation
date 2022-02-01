using System.Text.Json.Serialization;

namespace Homeautomation.Service.Settings;
public class DeviceOptions
{
    public const string Device = "Device";

    [JsonPropertyName("Id")]
    public string? Id
    {
        get; set;
    }

    [JsonPropertyName("Type")]
    public string? Type
    {
        get; set;
    }
}
