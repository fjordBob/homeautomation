using Homeautomation.Service.Models;

namespace Homeautomation.Service.Provider;

public interface ITemperatureHumidityProvider
{
    public Task<List<TemperatureHumidityHistory>> GetTemperatureHumidityHistoryAsync();

    public Task<List<TemperatureHumidity>> GetTemperatureHumidityAsync(string deviceId);

    public Task CreateTemperatureHumidityAsync(string deviceId, TemperatureHumidity temperatureHumidity);
}

