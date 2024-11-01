
# Используем базовый образ для ASP.NET
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

EXPOSE 8080
EXPOSE 8081
ENV ASPNETCORE_URLS=http://+:8080;https://+:8081

COPY ["SpaApp/SpaApp.csproj", "SpaApp/"]
COPY ["SpaApp.Application/SpaApp.Application.csproj", "SpaApp.Application/"]
COPY ["SpaApp.Domain/SpaApp.Domain.csproj", "SpaApp.Domain/"]
COPY ["SpaApp.Infrastructure/SpaApp.Infrastructure.csproj", "SpaApp.Infrastructure/"]
RUN dotnet restore "SpaApp/SpaApp.csproj" --verbosity detailed


# Копируем остальные файлы из бэкенда
COPY SpaApp/. ./SpaApp/
WORKDIR "/SpaApp"
RUN dotnet build "SpaApp.csproj" -c Release -o /app/build
RUN dotnet publish "SpaApp.csproj" -c Release -o /app/publish

# Публикация бэкенда
FROM build AS publish
#RUN dotnet publish "SpaApp/SpaApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
RUN dotnet publish "SpaApp/SpaApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the official .NET 8 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
#COPY https/aspnetapp.pfx /https/

ENTRYPOINT ["dotnet", "SpaApp.dll"]

