using AutoMapper;
using Homeautomation.Service.Controllers;
using Homeautomation.Service.Mappers;
using Homeautomation.Service.Tests.MoqDependencySetup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Homeautomation.Service.Tests;

[TestClass]
public class TemperatureHumidityControllerTest
{
    private static IMapper mapper;

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
}
