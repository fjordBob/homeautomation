FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/service/Service.csproj", "src/service/"]
RUN dotnet restore "./src/service/Service.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./src/service/Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./src/service/Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Homeautomation.Service.dll"]
