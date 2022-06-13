using Homeautomation.Service.Provider;
using Homeautomation.Service.Tests.MoqDependencySetup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;
using System.Linq;
using Polly;

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
       Assert.IsTrue(temperatures.Any(x => x.Id == 1), "No temperature item with id 1 found.");
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
            Temperature = "23",
            TimeStamp = new DateTime(2020, 5, 23, 3, 20, 0)
        }).Wait();

        sut.CreateTemperatureHumidityAsync("temperature_office", new Models.TemperatureHumidity
        {
            Humidity = "45",
            Id = 2,
            Temperature = "22.3",
            TimeStamp = new DateTime(2020, 5, 23, 4, 20, 0)
        }).Wait();

        sut.CreateTemperatureHumidityAsync("temperature_office", new Models.TemperatureHumidity
        {
            Humidity = "45",
            Id = 3,
            Temperature = "34",
            TimeStamp = new DateTime(2020, 5, 23, 1, 20, 0)
        }).Wait();

        Models.TemperatureHumidity? temperatures = sut.GetLatestTemperatureHumidityAsync("temperature_office").Result;
        Assert.IsTrue(temperatures != null, "No temperature item found (GetLatestTemperatureHumidityAsync).");
        Assert.IsTrue(temperatures.Id == 2, "No temperature item found (GetLatestTemperatureHumidityAsync) with id 2 found.");
    }

    [TestMethod]
    public void GetLatestTemperatureHumidityAsync_OnEmptyDatabase()
    {
        var sut = new TemperatureHumidityProvider(LoggerMoq.GetLogger<TemperatureHumidityProvider>(),
                                                    DeviceOptionsMoq.GetDeviceOptions(DeviceOptionsMoq.MoqOptions.Valid));

        var temperatures = sut.GetLatestTemperatureHumidityAsync("temperature_office").Result;
        Assert.IsTrue(temperatures == null, "Temperature item is not null (GetLatestTemperatureHumidityAsync).");
    }

    [TestCleanup]
    public void DoCleanup()
    {
        string? pathToExecutingDirectory = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
        if (pathToExecutingDirectory == null)
        {
            throw new DirectoryNotFoundException("No path to executing assembly found.");
        }

        string pathToDbFile = Path.Combine(pathToExecutingDirectory, "data");
        string fullFilePathToDbFile = Path.Combine(pathToDbFile, "homeautomation.db");

        Policy
            .Handle<IOException>()
            .WaitAndRetry(1, _ => TimeSpan.FromSeconds(1))
            .Execute(() =>
            {
                if (File.Exists(fullFilePathToDbFile))
                {
                    File.Delete(fullFilePathToDbFile);
                }
            });
    }
}


