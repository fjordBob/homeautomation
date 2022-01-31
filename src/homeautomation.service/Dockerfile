#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS builder
# Setup working directory for project
WORKDIR /app

# Copy project files
COPY . ./homeautomation.service/

# Restore & Build project with Release configuration
RUN dotnet build -c Release ./homeautomation.service/Homeautomation.Service.csproj

# Publish project to output folder
# and no build, as we did it already
WORKDIR /app/homeautomation.service
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
COPY --from=builder /app/homeautomation.service/out .

# Set URL that App will be exposed
ENV ASPNETCORE_URLS="http://*:7000"

# sets entry point command to automatically
# run application on `docker run`
ENTRYPOINT ["dotnet", "Homeautomation.Service.dll"]
