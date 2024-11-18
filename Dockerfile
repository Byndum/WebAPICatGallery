FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
LABEL author=janand02@easv365.dk
WORKDIR /app
EXPOSE 8080

ENV DOTNET_URLS=http://+:8080/

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebAPIParcelTracking.csproj", "./"]
RUN dotnet restore "WebAPIParcelTracking.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "WebAPIParcelTracking.csproj"

FROM build AS publish
RUN dotnet publish "WebAPIParcelTracking.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPIParcelTracking.dll"]