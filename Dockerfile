# 1. Етап збірки (використовуємо образ з SDK)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копіюємо файли проєктів (Web і Core)
COPY ["ConferenceManager.Web/ConferenceManager.Web.csproj", "ConferenceManager.Web/"]
COPY ["ConferenceManager.Core/ConferenceManager.Core.csproj", "ConferenceManager.Core/"]

# Відновлюємо залежності
RUN dotnet restore "ConferenceManager.Web/ConferenceManager.Web.csproj"

# Копіюємо решту файлів і збираємо проєкт
COPY . .
WORKDIR "/src/ConferenceManager.Web"
RUN dotnet build "ConferenceManager.Web.csproj" -c Release -o /app/build

# Публікуємо проєкт (створюємо готовий .dll)
FROM build AS publish
RUN dotnet publish "ConferenceManager.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 2. Етап запуску (використовуємо легкий образ ASP.NET)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Відкриваємо порт 8080 (стандарт для .NET 8 в Docker)
EXPOSE 8080

# Команда запуску
ENTRYPOINT ["dotnet", "ConferenceManager.Web.dll"]