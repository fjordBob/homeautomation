FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80

COPY ./bin/Release/net6.0/publish .
ENTRYPOINT ["dotnet", "Homeautomation.Frontend.dll"]
