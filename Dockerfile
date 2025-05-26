FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["MonolitoBackend.Api/MonolitoBackend.Api.csproj", "MonolitoBackend.Api/"]
COPY ["MonolitoBackend.Core/MonolitoBackend.Core.csproj", "MonolitoBackend.Core/"]
COPY ["MonolitoBackend.Infrastructure/MonolitoBackend.Infrastructure.csproj", "MonolitoBackend.Infrastructure/"]
RUN dotnet restore "MonolitoBackend.Api/MonolitoBackend.Api.csproj"

# Copy all files and build app
COPY . .
WORKDIR "/src/MonolitoBackend.Api"
RUN dotnet build "MonolitoBackend.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MonolitoBackend.Api.csproj" -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MonolitoBackend.Api.dll"]

