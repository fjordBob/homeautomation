using Homeautomation.Service.Models;
using Homeautomation.Service.Provider;
using Moq;
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
                        Temperature = "45",
                        Id = 1,
                        Humidity = "70",
                        TimeStamp = System.DateTime.Now
                    }
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
                                Temperature = "45",
                                Id = 1,
                                Humidity = "70",
                                TimeStamp = System.DateTime.Now
                            },
                            new TemperatureHumidity
                            {
                                Temperature = "45",
                                Id = 2,
                                Humidity = "70",
                                TimeStamp = System.DateTime.Now
                            }
                        }
                    }
                });
        mockRepo.Setup(repo => repo.CreateTemperatureHumidityAsync("someDevicem", new TemperatureHumidity()));

        return mockRepo.Object;
    }
}

