FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
ARG source
WORKDIR /app
EXPOSE 80

COPY bin/Release/net6.0/publish .
ENTRYPOINT ["dotnet", "Homeautomation.Service.dll"]
