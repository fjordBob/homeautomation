﻿namespace Homeautomation.Service.Models;
public class TemperatureHumidityHistory
{
    public string? DeviceId
    {
        get; set;
    }

    public List<TemperatureHumidity>? Values
    {
        get; set;
    }
}
