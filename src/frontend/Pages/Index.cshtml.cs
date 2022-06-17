using Frontend.Dtos;
using Frontend.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace frontend.Pages
{
    [IgnoreAntiforgeryToken] 
    public class IndexModel : PageModel
    {
        private static readonly HttpClient client = new();

        public IEnumerable<string?>? CaptureDate
        {
            get; private set;
        }

        public IEnumerable<double>? CaptureTemperature
        {
            get; private set;
        }

        private ILogger<IndexModel> Logger
        {
            get;
        }

        private ServiceSettingsOptions ServiceSettingsOptions
        {
            get;
        }

        public IndexModel(ILogger<IndexModel> logger, IOptions<ServiceSettingsOptions> serviceSettingsOptions)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            ServiceSettingsOptions = serviceSettingsOptions.Value ?? throw new ArgumentNullException(nameof(serviceSettingsOptions));
        }

        public async void OnGet()
        {
            await FetchTemperatureHumidity();
        }

        private async Task FetchTemperatureHumidity()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // We add a fix path here. Just for testing. Later on we add the possibility to change the devices.
            HttpResponseMessage response = await client.GetAsync($"{ServiceSettingsOptions.EndpointToUse}/api/temperaturehumidity/temperature_office");
            if (response.IsSuccessStatusCode)
            {
                List<TemperatureHumidityDto>? temperatureHumidities = JsonSerializer.Deserialize<List<TemperatureHumidityDto>>(response.Content.ReadAsStringAsync().Result);

                if (temperatureHumidities == null)
                {
                    return;
                }

                temperatureHumidities = temperatureHumidities
                         .Where(p => p.TimeStamp.HasValue)
                         .OrderBy(p =>
                         {
                             return p.TimeStamp == null ? DateTime.MinValue : p.TimeStamp.Value;
                         })
                         .ToList();

                CaptureDate = temperatureHumidities.Select(entry => entry.TimeStamp.ToString());
                CaptureTemperature = temperatureHumidities.Select(entry =>
                {
                    return entry.Temperature == null ? 0.0 : double.Parse(entry.Temperature, CultureInfo.InvariantCulture);
                });
            }
            else
            {
                Logger.LogError("Fetch values failed.");
            }
        }
    }
}