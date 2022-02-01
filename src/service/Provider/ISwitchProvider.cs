using Homeautomation.Service.Models;

namespace Homeautomation.Service.Provider;

public interface ISwitchProvider
{
    public Task<List<SwitchHistory>> GetSwitchHistoryAsync();

    public Task<List<Switch>> GetSwitchAsync(string deviceId);

    public Task CreateSwitchAsync(string deviceId, Switch @switch);
}

