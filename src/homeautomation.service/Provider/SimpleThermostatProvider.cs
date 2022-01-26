using Homeautomation.Service.Dtos;
using Homeautomation.Service.Models;
using Homeautomation.Service.Settings;
using LiteDB.Async;
using Microsoft.Extensions.Options;

namespace Homeautomation.Service.Provider;

public class SimpleThermostatProvider
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

    public SimpleThermostatProvider(ILogger<TemperatureHumidityProvider> logger, IOptions<DevicesOptions> devicesConfiguration)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        Devices = devicesConfiguration.Value ?? throw new ArgumentNullException(nameof(devicesConfiguration));
        Database = new LiteDatabaseAsync("Filename=homeautomation.db;Connection=shared;Password=hunter2");
    }

    public async Task<List<SimpleThermostatHistory>> GetSimpleThermostatHistoryAsync()
    {
        var collection = Database.GetCollection<SimpleThermostatHistory>();
        return await collection.Query().ToListAsync();
    }

    public async Task<List<SimpleThermostat>> GetSimpleThermostatAsync(string deviceId)
    {
        var collection = Database.GetCollection<SimpleThermostatHistory>();
        SimpleThermostatHistory? simpleThermostatHistory = await collection.Query().Where(x => x.DeviceId == deviceId).SingleAsync();

        return simpleThermostatHistory.Values ?? new List<SimpleThermostat>();
    }

    public async Task CreateSimpleThermostatAsync(string deviceId, SimpleThermostat simpleThermostat)
    {
        var collection = Database.GetCollection<SimpleThermostatHistory>();

        if (await collection.CountAsync(x => x.DeviceId == deviceId) == 0)
        {
            SimpleThermostatHistory simpleThermostatHistory = new SimpleThermostatHistory()
            {
                DeviceId = deviceId,
                Values = new List<SimpleThermostat>() { simpleThermostat }
            };

            await collection.InsertAsync(simpleThermostatHistory);
        }
        else
        {
            SimpleThermostatHistory simpleThermostatHistory = await collection.Query().FirstAsync();

            if (simpleThermostatHistory.Values == null)
            {
                throw new ArgumentException("SimpleThermostatList is empty but shouldn't");
            }

            simpleThermostatHistory.Values.Add(simpleThermostat);
            await collection.UpdateAsync(simpleThermostatHistory);
        }
    }
}

