using Homeautomation.Service.Settings;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;

namespace Homeautomation.Service.Tests.MoqDependencySetup;

internal static class DeviceOptionsMoq
{
    public enum MoqOptions
    {
        Valid,
        NoThermostat,
        NoTemperature,
        Empty
    }

    public static IOptions<DevicesOptions> GetDeviceOptions(MoqOptions moqOptions)
    {
        IOptions<DevicesOptions> retVal;

        switch (moqOptions)
        {
            case MoqOptions.Valid:
                retVal = Options.Create(new DevicesOptions
                {
                    DevicesList = new List<DeviceOptions>()
                    {
                        new DeviceOptions
                        {
                            Id = "temperature_office",
                            Type = "Temperature"
                        },
                        new DeviceOptions
                        {
                            Id = "thermostat_office",
                            Type = "Thermostat"
                        },
                        new DeviceOptions
                        {
                            Id = "switch_office",
                            Type = "Switch"
                        }
                    }
                });
                break;
            case MoqOptions.NoThermostat:
                retVal = Options.Create(new DevicesOptions
                {
                    DevicesList = new List<DeviceOptions>()
                    {
                        new DeviceOptions
                        {
                            Id = "temperature_office",
                            Type = "Temperature"
                        },
                        new DeviceOptions
                        {
                            Id = "switch_office",
                            Type = "Switch"
                        }
                    }
                });
                break;
            case MoqOptions.NoTemperature:
                retVal = Options.Create(new DevicesOptions
                {
                    DevicesList = new List<DeviceOptions>()
                    {
                        new DeviceOptions
                        {
                            Id = "thermostat_office",
                            Type = "Thermostat"
                        },
                        new DeviceOptions
                        {
                            Id = "switch_office",
                            Type = "Switch"
                        }
                    }
                });
                break;
            case MoqOptions.Empty:
                retVal = Options.Create(new DevicesOptions
                {
                    DevicesList = new List<DeviceOptions>()
                });
                break;
            default:
                retVal = Options.Create(new DevicesOptions
                {
                    DevicesList = new List<DeviceOptions>()
                });
                break;        
        }

        return retVal;
    }
}
