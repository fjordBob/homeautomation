using Homeautomation.Service.Models;

namespace Homeautomation.Service.Provider;

public interface ISimpleThermostatProvider
{
    public Task<List<SimpleThermostatHistory>> GetSimpleThermostatHistoryAsync();

    public Task<List<SimpleThermostat>> GetSimpleThermostatAsync(string deviceId);

    public Task CreateSimpleThermostatAsync(string deviceId, SimpleThermostat simpleThermostat);
}
