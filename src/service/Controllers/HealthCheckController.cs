using Homeautomation.Service.Settings;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace Homeautomation.Service.Controllers;

public class HealthCheckController : IHealthCheck
{
    private DevicesOptions Devices
    {
        get;
    }

    public HealthCheckController(IOptions<DevicesOptions> devicesConfiguration)
    {
        Devices = devicesConfiguration.Value;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Devices == null
                                ? HealthCheckResult.Unhealthy()
                                : HealthCheckResult.Healthy());
    }
}
