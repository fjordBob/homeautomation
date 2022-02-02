# Homeautomation
I have a few [zigbee](https://en.wikipedia.org/wiki/Zigbee) devices in my flat that communicate with a [MQTT](https://en.wikipedia.org/wiki/MQTT) instance. For some reasons i need the possibility to save certain values of my zigbee devices and show them in a timeline. This is what tihs project is for.

Right now there is only a service that you can feed up with values via REST-Api's

# REST-API overview
There is a Postman-Collection available [here](https://documenter.getpostman.com/view/3711527/UVeFLRQc). 

If you run this service in docker or your local machine you will see under http://[...]:YOUR-PORT/index.html a swagger documentation of the API.

# Used components
* This is a [ASP.NET Core](https://en.wikipedia.org/wiki/ASP.NET_Core) service running 
[dotnet core](https://dotnet.microsoft.com/en-us/) version 6.0.
* [AutoMapper](https://docs.automapper.org/en/stable/#) for mapping around with the DTOs and models.
* [LiteDB](https://www.litedb.org/) for storing the values. MongoDB because i want to play around with.
* Also some [actions for GitHub](https://docs.github.com/en/actions) are used for building, testing and dockerizing the service.

# Setup
Setup for this service is super easy three ways there are:
## Visual Studio 2022
Vs2022 is needed because of dotnet core 6.0. The rest is easy:
* Clone the repo.
* Open the homeautomation.sln file with VS2022.
* Build and run.
* Service runs on your local machine

## Bash
* Clone the repo
* Open a bash of your choice.
* Type in ``dotnet build ./PATH-TO-CSPROJ/Service.csproj``

## Docker hub
If you just want the docker container you can do a ``docker pull fjordbob/homeautomation:latest``

After a succesfully build you can put the artefacts into a docker container. Should run without issues. This project contains already a Dockerfile which will do the trick. Have a look [here](https://docs.docker.com/engine/reference/commandline/build/#build-with--) to see how to call works.

# Testing
There are a few uTests available that runs in the CI pipeline also. As with the build of the service, you also have the option of choosing between VS2022 and bash during the tests. 

## Visual Studio 2022
Vs2022 is needed because of dotnet core 6.0. I assume you did the build already with VS2022 so now just click 'Run all tests' in the Test Explorer.

## Bash
* I assume you did the build already with the bash.
* Open a bash of your choice.
* Type in ``dotnet test ./PATH-TO-CSPROJ/Tests.csproj``

# Configuration
There is not that much on configuration you WILL NEED to do. You need some devices configured so that you will know what values to what devices. This is how a appsettings.json might looks like:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Devices": [
    {
      "Id": "temperature_office",
      "Type": "Temperature"
    },
    {
      "Id": "temperature_living",
      "Type": "Temperature"
    },
    {
      "Id": "simpleThermostat_office",
      "Type": "SimpleThermostat"
    },
    {
      "Id": "heatingValve_office",
      "Type": "Switch"
    }
  ]
}

```

You can add as many devices as you want. Right now there are three devices available: **Swich**, **SimpleThermostat** and **Temperature**

# Road Map
The goal of this service is to show you the save value in a graph to show you the values of temperature and when a switch gets active or not. For this we need a UI. The next step is a service that contains the UI.