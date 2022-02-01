using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Homeautomation.Service.Controllers;

public class HealthCheckController : ControllerBase
{
    [HttpGet]
    [Route("healthCheck/version")]
    public string GetVersion()
    {
        return "1.0.0.0";
    }

    [HttpGet]
    [Route("healthCheck/ping")]
    public bool Ping()
    {
        return true;
    }
}
