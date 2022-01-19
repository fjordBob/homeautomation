namespace Homeautomation.Service.Models;

public interface IDevice
{
    string? Id
    {
        get; set;
    }

    string? Name
    {
        get; set;
    }

    Devices DeviceType
    {
        get; set;
    }
}

