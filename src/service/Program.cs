using Homeautomation.Service.Provider;
using Homeautomation.Service.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

builder.Services.Configure<DevicesOptions>(x => x.DevicesList = new List<DeviceOptions>(builder.Configuration.GetSection("Devices").Get<DeviceOptions[]>()));
builder.Services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNameCaseInsensitive = true);
builder.Services.AddOptions();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(x => 
    {
        string returnedValue = x.Name;
        if (returnedValue.EndsWith("Dto"))
        {
            returnedValue = returnedValue.Replace("Dto", string.Empty);
        }

        return returnedValue;
    });
});
builder.Services.AddRouting(options => options.LowercaseUrls = true);
// Add services to the container.
builder.Services.AddTransient<ITemperatureHumidityProvider, TemperatureHumidityProvider>();
builder.Services.AddTransient<ISimpleThermostatProvider, SimpleThermostatProvider>();
builder.Services.AddTransient<ISwitchProvider, SwitchProvider>();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.UseStaticFiles();
app.MapHealthChecks("/healthCheck");
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint($"swagger/v1/swagger.json", "Homeautomation v1");
    c.RoutePrefix = string.Empty;
});
app.MapHealthChecks("/healthCheck");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
