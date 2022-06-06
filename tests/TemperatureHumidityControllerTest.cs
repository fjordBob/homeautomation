using AutoMapper;
using Homeautomation.Service.Controllers;
using Homeautomation.Service.Mappers;
using Homeautomation.Service.Tests.MoqDependencySetup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;

namespace Homeautomation.Service.Tests;

[TestClass]
public class TemperatureHumidityControllerTest
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static IMapper mapper;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public TemperatureHumidityControllerTest()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new SimpleThermostatHistoryProfile());
            mc.AddProfile(new SimpleThermostatProfile());
            mc.AddProfile(new SwitchHistoryProfile());
            mc.AddProfile(new SwitchProfile());
            mc.AddProfile(new TemperatureHumidityHistoryProfile());
            mc.AddProfile(new TemperatureHumidityProfile());
        });

        mapper = mappingConfig.CreateMapper();
    }

    [TestMethod]
    public void GetTemperatureHumidityAsync_Valid()
    {
        var sut = new TemperatureHumidityController(LoggerMoq.GetLogger<TemperatureHumidityController>(),
                                                    DeviceOptionsMoq.GetDeviceOptions(DeviceOptionsMoq.MoqOptions.Valid),
                                                    TemperatureHumidityProviderMoq.GetProvider(), mapper);

        var result = sut.GetTemperatureHumidityAsync("someDevice").Result;
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public void GetTemperatureHumidityHistoryAsync_Valid()
    {
        var sut = new TemperatureHumidityController(LoggerMoq.GetLogger<TemperatureHumidityController>(),
                                                    DeviceOptionsMoq.GetDeviceOptions(DeviceOptionsMoq.MoqOptions.Valid),
                                                    TemperatureHumidityProviderMoq.GetProvider(), mapper);

        var result = sut.GetTemperatureHumidityAsync().Result;
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public void PostTemperatureHumidityAsync_Valid()
    {
        var sut = new TemperatureHumidityController(LoggerMoq.GetLogger<TemperatureHumidityController>(),
                                                    DeviceOptionsMoq.GetDeviceOptions(DeviceOptionsMoq.MoqOptions.Valid),
                                                    TemperatureHumidityProviderMoq.GetProvider(), mapper);

        var result = sut.PostTemperatureHumidityAsync("", new Dtos.TemperatureHumidityDto()).Result;
        var okResult = result as NoContentResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(204, okResult.StatusCode);
    }

    [TestCleanup]
    public void DoCleanup()
    {
        string? pathToExecutingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        if (pathToExecutingDirectory == null)
        {
            throw new DirectoryNotFoundException("No path to executing assembly found.");
        }

        string pathToDbFile = Path.Combine(pathToExecutingDirectory, "data");
        string fullFilePathToDbFile = Path.Combine(pathToDbFile, "homeautomation.db");

        if (File.Exists(fullFilePathToDbFile))
        {
            File.Delete(fullFilePathToDbFile);
        }
    }
}
