using AutoMapper;
using Homeautomation.Service.Dtos;
using Homeautomation.Service.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Homeautomation.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeviceController : ControllerBase
{
    private ILogger<DeviceController> Logger
    {
        get;
    }

    private DevicesOptions Devices
    {
        get;
    }

    public DeviceController(ILogger<DeviceController> logger, IOptions<DevicesOptions> devicesConfiguration)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        Devices = devicesConfiguration.Value ?? throw new ArgumentNullException(nameof(devicesConfiguration));     
    }

    [HttpGet]
    public IActionResult GetDevices()
    {
        if (Devices.DevicesList == null)
        {
            return NotFound("No devices configured. Ask admin.");
        }

        List<DeviceOutDto> retVal = new();
        foreach (DeviceOptions? item in Devices.DevicesList)
        {
            retVal.Add(new DeviceOutDto 
            {
                Id = item.Id,
                Type = item.Type                    
            });
        }

        return Ok(retVal);
    }
}
