# Dockerfile optimizado para producción
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Crear usuario no-root para seguridad
RUN adduser --disabled-password --gecos '' --uid 1000 dotnetuser
USER dotnetuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos de proyecto para mejor cache de layers
COPY ["customhost.platform.API/customhost.platform.API.csproj", "customhost.platform.API/"]
COPY ["crm/crm.csproj", "crm/"]
COPY ["billings/billings.csproj", "billings/"]
COPY ["GuestExperience/GuestExperience.csproj", "GuestExperience/"]
COPY ["profiles/profiles.csproj", "profiles/"]
COPY ["analytics/analytics.csproj", "analytics/"]
COPY ["Shared/Shared.csproj", "Shared/"]

# Restore dependencias
RUN dotnet restore "customhost.platform.API/customhost.platform.API.csproj"

# Copiar código fuente
COPY . .

# Build en modo Release
WORKDIR "/src/customhost.platform.API"
RUN dotnet build "customhost.platform.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "customhost.platform.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

# Cambiar de vuelta a root temporalmente para copiar archivos
USER root
COPY --from=publish /app/publish .

# Crear directorio para logs y establecer permisos
RUN mkdir -p /app/logs && chown -R dotnetuser:dotnetuser /app/logs

# Volver a usuario no-root
USER dotnetuser

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=60s --retries=3 \
  CMD curl -f http://localhost:80/health || exit 1

ENTRYPOINT ["dotnet", "customhost.platform.API.dll"]
