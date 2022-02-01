using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Homeautomation.Service.Tests.MoqDependencySetup;

internal static class LoggerMoq
{
    public static ILogger<T> GetLogger<T>() where T : ControllerBase
    {
        var loggerMock = new Mock<ILogger<T>>();
        return loggerMock.Object;
    }
}
