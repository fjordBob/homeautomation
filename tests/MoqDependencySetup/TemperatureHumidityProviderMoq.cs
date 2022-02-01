using Homeautomation.Service.Provider;
using Moq;

namespace Tests.MoqDependencySetup;
internal static  class TemperatureHumidityProviderMoq
{
    public static void GetTemperatureHumidity()
    {
        var mockRepo = new Mock<TemperatureHumidityProvider>();
        mockRepo.Setup(repo => repo.GetTemperatureHumidityAsync(""))
            .ReturnsAsync(new System.Collections.Generic.List<Homeautomation.Service.Models.TemperatureHumidity>());

        var o = mockRepo.Object;
    }
}

