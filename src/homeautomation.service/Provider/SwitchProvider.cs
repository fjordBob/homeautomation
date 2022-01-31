using Homeautomation.Service.Dtos;
using Homeautomation.Service.Models;
using Homeautomation.Service.Settings;
using LiteDB.Async;
using Microsoft.Extensions.Options;

namespace Homeautomation.Service.Provider;

public class SwitchProvider
{
    private ILogger<TemperatureHumidityProvider> Logger
    {
        get;
    }

    private DevicesOptions Devices
    {
        get;
    }

    private LiteDatabaseAsync Database
    {
        get;
    }

    public SwitchProvider(ILogger<TemperatureHumidityProvider> logger, IOptions<DevicesOptions> devicesConfiguration)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        Devices = devicesConfiguration.Value ?? throw new ArgumentNullException(nameof(devicesConfiguration));
        DirectoryInfo? dbDir = Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "data"));
        string? path = Path.Combine(dbDir.FullName, "homeautomation.db");
        Database = new LiteDatabaseAsync($"Filename={path};Connection=shared;Password=hunter2");
    }

    public async Task<List<SwitchHistory>> GetSwitchHistoryAsync()
    {
        var collection = Database.GetCollection<SwitchHistory>();
        return await collection.Query().ToListAsync();
    }

    public async Task<List<Switch>> GetSwitchAsync(string deviceId)
    {
        var collection = Database.GetCollection<SwitchHistory>();
        SwitchHistory? switchHistory = await collection.Query().Where(x => x.DeviceId == deviceId).SingleAsync();

        return switchHistory.Values ?? new List<Switch>();
    }

    public async Task CreateSwitchAsync(string deviceId, Switch @switch)
    {
        var collection = Database.GetCollection<SwitchHistory>();

        if (await collection.CountAsync(x => x.DeviceId == deviceId) == 0)
        {
            SwitchHistory switchHistory = new SwitchHistory()
            {
                DeviceId = deviceId,
                Values = new List<Switch>() { @switch }
            };

            await collection.InsertAsync(switchHistory);
        }
        else
        {
            SwitchHistory switchHistory = await collection.Query().FirstAsync();

            if (switchHistory.Values == null)
            {
                throw new ArgumentException("SwitchList is empty but shouldn't");
            }

            switchHistory.Values.Add(@switch);
            await collection.UpdateAsync(switchHistory);
        }
    }
}

