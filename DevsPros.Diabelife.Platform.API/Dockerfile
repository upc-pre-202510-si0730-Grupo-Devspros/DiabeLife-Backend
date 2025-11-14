# Use the official .NET 9 runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Use the official .NET 9 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["DevsPros.Diabelife.Platform.API.csproj", "."]
RUN dotnet restore "DevsPros.Diabelife.Platform.API.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "DevsPros.Diabelife.Platform.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DevsPros.Diabelife.Platform.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevsPros.Diabelife.Platform.API.dll"]