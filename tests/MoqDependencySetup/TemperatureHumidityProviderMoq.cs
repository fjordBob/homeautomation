using Homeautomation.Service.Models;
using Homeautomation.Service.Provider;
using Moq;
using System;
using System.Collections.Generic;

namespace Homeautomation.Service.Tests.MoqDependencySetup;
internal static  class TemperatureHumidityProviderMoq
{
    public static ITemperatureHumidityProvider GetProvider()
    {
        var mockRepo = new Mock<ITemperatureHumidityProvider>();

        mockRepo.Setup(repo => repo.GetTemperatureHumidityAsync("someDevice"))
                .ReturnsAsync(new List<TemperatureHumidity> 
                { 
                    new TemperatureHumidity 
                    {
                        Temperature = "20.3",
                        Id = 1,
                        Humidity = "70",
                        TimeStamp = new DateTime(2020, 5, 23, 1, 20, 0)
                    }
                });
        mockRepo.Setup(repo => repo.GetLatestTemperatureHumidityAsync("temperature_office"))
                .ReturnsAsync(new TemperatureHumidity
                    {
                        Temperature = "20.3",
                        Id = 1,
                        Humidity = "70",
                        TimeStamp = new DateTime(2020, 5, 23, 1, 20, 0)
                    });
        mockRepo.Setup(repo => repo.GetTemperatureHumidityHistoryAsync())
                .ReturnsAsync(new List<TemperatureHumidityHistory> 
                {
                    new TemperatureHumidityHistory 
                    {
                        Id = 1,
                        DeviceId = "someDevice",
                        Values = new List<TemperatureHumidity>
                        {
                            new TemperatureHumidity
                            {
                                Temperature = "20.3",
                                Id = 1,
                                Humidity = "70",
                                TimeStamp = new DateTime(2020, 5, 23, 1, 20, 0)
                            },
                            new TemperatureHumidity
                            {
                                Temperature = "23.5",
                                Id = 2,
                                Humidity = "70",
                                TimeStamp = new DateTime(2020, 5, 23, 1, 10, 0)
                            }
                        }
                    }
                });
        mockRepo.Setup(repo => repo.CreateTemperatureHumidityAsync("someDevicem", new TemperatureHumidity()));

        return mockRepo.Object;
    }
}

