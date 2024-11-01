
# Используем базовый образ для ASP.NET
//FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Используем SDK образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем файл проекта бэкенда
COPY ["SpaApp/SpaApp.csproj", "SpaApp/"]

# Восстанавливаем зависимости только для бэкенда
RUN dotnet restore "SpaApp/SpaApp.csproj"

# Копируем остальные файлы из бэкенда
COPY SpaApp/. ./SpaApp/

# Сборка бэкенда
RUN dotnet build "SpaApp/SpaApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Публикация бэкенда
FROM build AS publish
RUN dotnet publish "SpaApp/SpaApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpaApp.dll"]