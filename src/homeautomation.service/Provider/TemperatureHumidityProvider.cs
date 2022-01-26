using Homeautomation.Service.Dtos;
using Homeautomation.Service.Models;
using Homeautomation.Service.Settings;
using LiteDB.Async;
using Microsoft.Extensions.Options;

namespace Homeautomation.Service.Provider;
public class TemperatureHumidityProvider
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

    public TemperatureHumidityProvider(ILogger<TemperatureHumidityProvider> logger, IOptions<DevicesOptions> devicesConfiguration)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        Devices = devicesConfiguration.Value ?? throw new ArgumentNullException(nameof(devicesConfiguration));
        Database = new LiteDatabaseAsync("Filename=homeautomation.db;Connection=shared;Password=hunter2");
    }

    public async Task<List<TemperatureHumidityHistory>> GetTemperatureHumidityHistoryAsync()
    {
        var collection = Database.GetCollection<TemperatureHumidityHistory>();
        return await collection.Query().ToListAsync();
    }

    public async Task<List<TemperatureHumidity>> GetTemperatureHumidityAsync(string deviceId)
    {
        var collection = Database.GetCollection<TemperatureHumidityHistory>();
        TemperatureHumidityHistory? temperatureHumidityHistory = await collection.Query().Where(x => x.DeviceId == deviceId).SingleAsync();

        return temperatureHumidityHistory.Values ?? new List<TemperatureHumidity>();
    }

    public async Task CreateTemperatureHumidityAsync(string deviceId, TemperatureHumidity temperatureHumidity)
    {
        var collection = Database.GetCollection<TemperatureHumidityHistory>();

        if (await collection.CountAsync(x => x.DeviceId == deviceId) == 0)
        {
            TemperatureHumidityHistory temperatureHumidityHistory = new TemperatureHumidityHistory()
            {
                DeviceId = deviceId,
                Values = new List<TemperatureHumidity>() { temperatureHumidity }
            };

            await collection.InsertAsync(temperatureHumidityHistory);
        }
        else
        {
            TemperatureHumidityHistory temperatureHumidityHistory = await collection.Query().FirstAsync();

            if (temperatureHumidityHistory.Values == null)
            {
                throw new ArgumentException("TemperatureHumidityList is empty but shouldn't");
            }

            temperatureHumidityHistory.Values.Add(temperatureHumidity);
            await collection.UpdateAsync(temperatureHumidityHistory);
        }
    }
}
