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
[Route("[controller]")]
public class SimpleThermostatController : ControllerBase
{
    private ILogger<TemperatureHumidityController> Logger
    {
        get;
    }

    private DevicesOptions Devices
    {
        get;
    }

    private SimpleThermostatProvider SimpleThermostatProvider
    {
        get;
    }

    private IMapper Mapper
    {
        get;
    }

    public SimpleThermostatController(ILogger<TemperatureHumidityController> logger, IOptions<DevicesOptions> devicesConfiguration,
                                            SimpleThermostatProvider simpleThermostatProvider, IMapper mapper)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        SimpleThermostatProvider = simpleThermostatProvider ?? throw new ArgumentNullException(nameof(simpleThermostatProvider));
        Devices = devicesConfiguration.Value ?? throw new ArgumentNullException(nameof(devicesConfiguration));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> GetSimpleThermostatAsync()
    {
        List<SimpleThermostatHistoryOutDto> retVal = new();
        foreach (SimpleThermostatHistory? item in await SimpleThermostatProvider.GetSimpleThermostatHistoryAsync())
        {
            retVal.Add(Mapper.Map<SimpleThermostatHistoryOutDto>(item));
        }

        return Ok(retVal);
    }

    [HttpGet]
    [Route("{deviceId}")]
    public async Task<IActionResult> GetSimpleThermostatAsync(string deviceId)
    {
        List<SimpleThermostatOutDto> retVal = new();
        foreach (SimpleThermostat? item in await SimpleThermostatProvider.GetSimpleThermostatAsync(deviceId))
        {
            retVal.Add(Mapper.Map<SimpleThermostatOutDto>(item));
        }

        return Ok(retVal);
    }

    [HttpPost]
    [Route("{deviceId}")]
    public async Task<IActionResult> PostSimpleThermostatAsync(string deviceId, [FromBody][Required] SimpleThermostatDto simpleThermostatDto)
    {
        if (Devices.DevicesList == null)
        {
            return StatusCode(500, "Internal Server Error. No device list defined. Contact admin.");
        }
        if (!Devices.DevicesList.Any(device => device.Id == deviceId))
        {
            return NoContent();
        }

        SimpleThermostat mappedSimpleThermostat = Mapper.Map<SimpleThermostat>(simpleThermostatDto);
        await SimpleThermostatProvider.CreateSimpleThermostatAsync(deviceId, mappedSimpleThermostat);

        return Ok();
    }
}