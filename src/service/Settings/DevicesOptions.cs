using System.Text.Json.Serialization;

namespace Homeautomation.Service.Settings;
public class DevicesOptions
{
    public const string Devices = "Devices";

    [JsonPropertyName("Devices")]
    public List<DeviceOptions>? DevicesList
    {
        get; set;
    }
}