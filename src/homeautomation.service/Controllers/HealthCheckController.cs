using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Homeautomation.Service.Controllers
{
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        [Route("healthCheck/version")]
        public string GetVersion()
        {
            System.Reflection.Assembly assembly = typeof(HealthCheckController).Assembly;
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

            return fvi.FileVersion;
        }

        [HttpGet]
        [Route("healthCheck/ping")]
        public bool Ping()
        {
            return true;
        }
    }
}
