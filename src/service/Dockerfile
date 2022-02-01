#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
ARG source
WORKDIR /app
EXPOSE 80

COPY src/service/bin/Release/net6.0/publish .
ENTRYPOINT ["dotnet", "Homeautomation.Service.dll"]
