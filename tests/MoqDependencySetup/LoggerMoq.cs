﻿using Homeautomation.Service.Provider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Homeautomation.Service.Tests.MoqDependencySetup;

internal static class LoggerMoq
{
    public static ILogger<T> GetLogger<T>() where T : class
    {
        var loggerMock = new Mock<ILogger<T>>();
        return loggerMock.Object;
    }
}
