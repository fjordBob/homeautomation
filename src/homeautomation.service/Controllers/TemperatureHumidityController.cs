﻿using AutoMapper;
using Homeautomation.Service.Dtos;
using Homeautomation.Service.Models;
using Homeautomation.Service.Provider;
using Homeautomation.Service.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Homeautomation.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class TemperatureHumidityController : ControllerBase
{
    private ILogger<TemperatureHumidityController> Logger
    {
        get;
    }

    private DevicesOptions Devices
    {
        get;
    }

    private TemperatureHumidityProvider TemperatureHumidityProvider
    {
        get;
    }

    private IMapper Mapper
    {
        get;
    }

    public TemperatureHumidityController(ILogger<TemperatureHumidityController> logger, IOptions<DevicesOptions> devicesConfiguration,
                                            TemperatureHumidityProvider temperatureHumidityProvider, IMapper mapper)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        TemperatureHumidityProvider = temperatureHumidityProvider ?? throw new ArgumentNullException(nameof(temperatureHumidityProvider));
        Devices = devicesConfiguration.Value ?? throw new ArgumentNullException(nameof(devicesConfiguration));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> GetTemperatureHumidityAsync()
    {
        List<TemperatureHumidityHistoryOutDto> retVal = new();
        foreach (TemperatureHumidityHistory? item in await TemperatureHumidityProvider.GetTemperatureHumidityHistoryAsync())
        {
            retVal.Add(Mapper.Map<TemperatureHumidityHistoryOutDto>(item));
        }
        
        return Ok(retVal);
    }

    [HttpGet]
    [Route("{deviceId}")]
    public async Task<IActionResult> GetTemperatureHumidityAsync(string deviceId)
    {
        List<TemperatureHumidityOutDto> retVal = new();
        foreach (TemperatureHumidity? item in await TemperatureHumidityProvider.GetTemperatureHumidityAsync(deviceId))
        {
            retVal.Add(Mapper.Map<TemperatureHumidityOutDto>(item));
        }

        return Ok(retVal);
    }

    [HttpPost]
    [Route("{deviceId}")]
    public async Task<IActionResult> PostTemperatureHumidityAsync(string deviceId, [FromBody][Required] TemperatureHumidityDto temperatureHumidityDto)
    {
        if (Devices.DevicesList == null)
        {
            return StatusCode(500, "Internal Server Error. No device list defined. Contact admin.");
        }
        if (!Devices.DevicesList.Any(device => device.Id == deviceId))
        {
            return NoContent();
        }

        TemperatureHumidity mappedTemperatureHumidity = Mapper.Map<TemperatureHumidity>(temperatureHumidityDto);
        await TemperatureHumidityProvider.CreateTemperatureHumidityAsync(deviceId, mappedTemperatureHumidity);

        return Ok();
    }
}
