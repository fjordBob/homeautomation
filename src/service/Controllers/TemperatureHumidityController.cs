using AutoMapper;
using Homeautomation.Service.Dtos;
using Homeautomation.Service.Models;
using Homeautomation.Service.Provider;
using Homeautomation.Service.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Homeautomation.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
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

    private ITemperatureHumidityProvider TemperatureHumidityProvider
    {
        get;
    }

    private IMapper Mapper
    {
        get;
    }

    public TemperatureHumidityController(ILogger<TemperatureHumidityController> logger, IOptions<DevicesOptions> devicesConfiguration,
                                            ITemperatureHumidityProvider temperatureHumidityProvider, IMapper mapper)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        TemperatureHumidityProvider = temperatureHumidityProvider ?? throw new ArgumentNullException(nameof(temperatureHumidityProvider));
        Devices = devicesConfiguration.Value ?? throw new ArgumentNullException(nameof(devicesConfiguration));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<TemperatureHumidityHistoryOutDto>), StatusCodes.Status200OK)]
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
    [ProducesResponseType(typeof(List<TemperatureHumidityOutDto>), StatusCodes.Status200OK)]
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
            return StatusCode(404, "No device id in configuration found. Contact admin.");
        }

        TemperatureHumidity mappedTemperatureHumidity = Mapper.Map<TemperatureHumidity>(temperatureHumidityDto);
        TemperatureHumidity? latestTemperatureHumidity = await TemperatureHumidityProvider.GetLatestTemperatureHumidityAsync(deviceId);

        if (latestTemperatureHumidity == null)  
        {
            await TemperatureHumidityProvider.CreateTemperatureHumidityAsync(deviceId, mappedTemperatureHumidity);
        }
        else
        {
            double mappedTemperature = mappedTemperatureHumidity.Temperature == null ? 0.0 : double.Parse(mappedTemperatureHumidity.Temperature);
            double latestTemperature = latestTemperatureHumidity.Temperature == null ? 0.0 : double.Parse(latestTemperatureHumidity.Temperature);

            // To avoid entries with a small delta temperature.
            if (Math.Abs(mappedTemperature - latestTemperature) > 0.5)
            {
                await TemperatureHumidityProvider.CreateTemperatureHumidityAsync(deviceId, mappedTemperatureHumidity);
            }
        }        

        return Ok();
    }
}
