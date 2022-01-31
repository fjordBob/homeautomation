#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/homeautomation.service/Homeautomation.Service.csproj", "src/homeautomation.service/"]
RUN dotnet restore "src/homeautomation.service/Homeautomation.Service.csproj"
COPY . .
WORKDIR "/src/homeautomation.service"
RUN dotnet build "Homeautomation.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Homeautomation.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Homeautomation.Service.dll"]