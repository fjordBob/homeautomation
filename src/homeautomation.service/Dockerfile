#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS builder
# Setup working directory for project
WORKDIR /app

COPY ./src/homeautomation.service/Homeautomation.Service.csproj ./src/homeautomation.service/

# Restore nuget packages
RUN dotnet restore "src/homeautomation.service/Homeautomation.Service.csproj"

# Copy project files
COPY ./src/homeautomation.service/ ./src/homeautomation.service/

# Build project with Release configuration
# and no restore, as we did it already
RUN dotnet build -c Release --no-restore .src/homeautomation.service/Homeautomation.Service.csproj

# Publish project to output folder
# and no build, as we did it already
WORKDIR /app/src/homeautomation.service
RUN ls
RUN dotnet publish -c Release --no-build -o out

########################################
#  Second stage of multistage build
########################################
#  Use other build image as the final one
#    that won't have source codes
########################################
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine

# Setup working directory for project
WORKDIR /app

# Copy published in previous stage binaries
# from the `builder` image
COPY --from=builder /app/src/homeautomation.service/out .

# Set URL that App will be exposed
ENV ASPNETCORE_URLS="http://*:7000"

# sets entry point command to automatically
# run application on `docker run`
ENTRYPOINT ["dotnet", "Homeautomation.Service.dll"]
