# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["DevsPros.Diabelife.Platform.API/DevsPros.Diabelife.Platform.API.csproj", "DevsPros.Diabelife.Platform.API/"]
RUN dotnet restore "DevsPros.Diabelife.Platform.API/DevsPros.Diabelife.Platform.API.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/DevsPros.Diabelife.Platform.API"
RUN dotnet build "DevsPros.Diabelife.Platform.API.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "DevsPros.Diabelife.Platform.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevsPros.Diabelife.Platform.API.dll"]