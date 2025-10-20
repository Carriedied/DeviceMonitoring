FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DeviceMonitoringAPI/DeviceMonitoringAPI.csproj", "DeviceMonitoringAPI/"]
RUN dotnet restore "./DeviceMonitoringAPI/./DeviceMonitoringAPI.csproj"
COPY . .
WORKDIR "/src/DeviceMonitoringAPI"
RUN dotnet build "./DeviceMonitoringAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DeviceMonitoringAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeviceMonitoringAPI.dll"]