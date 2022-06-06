using Homeautomation.Service.Provider;
using Homeautomation.Service.Tests.MoqDependencySetup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;
using System.Linq;

namespace Homeautomation.Service.Tests;

[TestClass]
public class TemperatureHumidityProviderTest
{
    public TemperatureHumidityProviderTest()
    {
    }

    [TestMethod]
    public void GetTemperatureHumidityAsync_OnEmptyDatabase()
    {
        var sut = new TemperatureHumidityProvider(LoggerMoq.GetLogger<TemperatureHumidityProvider>(),
                                                    DeviceOptionsMoq.GetDeviceOptions(DeviceOptionsMoq.MoqOptions.Valid));
        Assert.ThrowsExceptionAsync<AggregateException>(async () =>
        {
            await sut.GetTemperatureHumidityHistoryAsync();
        });
    }

    [TestMethod]
    public void GetTemperatureHumidityAsync_Valid_OneEntry()
    {
        var sut = new TemperatureHumidityProvider(LoggerMoq.GetLogger<TemperatureHumidityProvider>(),
                                                    DeviceOptionsMoq.GetDeviceOptions(DeviceOptionsMoq.MoqOptions.Valid));
        sut.CreateTemperatureHumidityAsync("temperature_office", new Models.TemperatureHumidity
        {
            Humidity = "45",
            Id = 1,
            Temperature = "34",
            TimeStamp = DateTime.Now
        }).Wait();

       var temperatures = sut.GetTemperatureHumidityAsync("temperature_office").Result;
       Assert.IsTrue(temperatures.Any(x => x.Id == 1));
    }

    [TestMethod]
    public void GetLatestTemperatureHumidityAsync_Valid()
    {
        var sut = new TemperatureHumidityProvider(LoggerMoq.GetLogger<TemperatureHumidityProvider>(),
                                                    DeviceOptionsMoq.GetDeviceOptions(DeviceOptionsMoq.MoqOptions.Valid));
        sut.CreateTemperatureHumidityAsync("temperature_office", new Models.TemperatureHumidity
        {
            Humidity = "45",
            Id = 1,
            Temperature = "34",
            TimeStamp = new DateTime(2020, 5, 23, 3, 20, 0)
        }).Wait();

        sut.CreateTemperatureHumidityAsync("temperature_office", new Models.TemperatureHumidity
        {
            Humidity = "45",
            Id = 2,
            Temperature = "34",
            TimeStamp = new DateTime(2020, 5, 23, 4, 20, 0)
        }).Wait();

        sut.CreateTemperatureHumidityAsync("temperature_office", new Models.TemperatureHumidity
        {
            Humidity = "45",
            Id = 3,
            Temperature = "34",
            TimeStamp = new DateTime(2020, 5, 23, 1, 20, 0)
        }).Wait();

        var temperatures = sut.GetLatestTemperatureHumidityAsync("temperature_office").Result;
        Assert.IsTrue(temperatures.Id == 2);
    }

    [TestMethod]
    public void GetLatestTemperatureHumidityAsync_OnEmptyDatabase()
    {
        var sut = new TemperatureHumidityProvider(LoggerMoq.GetLogger<TemperatureHumidityProvider>(),
                                                    DeviceOptionsMoq.GetDeviceOptions(DeviceOptionsMoq.MoqOptions.Valid));

        var temperatures = sut.GetLatestTemperatureHumidityAsync("temperature_office").Result;
        Assert.IsTrue(temperatures == null);
    }

    [TestCleanup]
    public void DoCleanup()
    {
        string codeBase = Assembly.GetExecutingAssembly().CodeBase;
        var uri = new UriBuilder(codeBase);
        string path = Uri.UnescapeDataString(uri.Path);
        string pathToDbFile = Path.Combine(Path.GetDirectoryName(path), "data");

        File.Delete(Path.Combine(pathToDbFile, "homeautomation.db"));
    }
}


