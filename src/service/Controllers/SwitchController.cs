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
public class SwitchController : ControllerBase
{
    private ILogger<SwitchController> Logger
    {
        get;
    }

    private DevicesOptions Devices
    {
        get;
    }

    private ISwitchProvider SwitchProvider
    {
        get;
    }

    private IMapper Mapper
    {
        get;
    }

    public SwitchController(ILogger<SwitchController> logger, IOptions<DevicesOptions> devicesConfiguration,
                                            ISwitchProvider switchProvider, IMapper mapper)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        SwitchProvider = switchProvider ?? throw new ArgumentNullException(nameof(switchProvider));
        Devices = devicesConfiguration.Value ?? throw new ArgumentNullException(nameof(devicesConfiguration));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<SwitchHistoryOutDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSwitchAsync()
    {
        List<SwitchHistoryOutDto> retVal = new();
        foreach (SwitchHistory? item in await SwitchProvider.GetSwitchHistoryAsync())
        {
            retVal.Add(Mapper.Map<SwitchHistoryOutDto>(item));
        }

        return Ok(retVal);
    }

    [HttpGet]
    [Route("{deviceId}")]
    [ProducesResponseType(typeof(List<SwitchOutDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSwitchAsync(string deviceId)
    {
        List<SwitchOutDto> retVal = new();
        foreach (Switch? item in await SwitchProvider.GetSwitchAsync(deviceId))
        {
            retVal.Add(Mapper.Map<SwitchOutDto>(item));
        }

        return Ok(retVal);
    }

    [HttpPost]
    [Route("{deviceId}")]
    public async Task<IActionResult> PostSwitchAsync(string deviceId, [FromBody][Required] SwitchDto switchDto)
    {
        if (Devices.DevicesList == null)
        {
            return StatusCode(500, "Internal Server Error. No device list defined. Contact admin.");
        }
        if (!Devices.DevicesList.Any(device => device.Id == deviceId))
        {
            return NoContent();
        }

        Switch mappedSwitch = Mapper.Map<Switch>(switchDto);
        await SwitchProvider.CreateSwitchAsync(deviceId, mappedSwitch);

        return Ok();
    }
}