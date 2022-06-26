FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
COPY . .
WORKDIR /src/Hosts/solarLab.Api
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS runtime
WORKDIR /app
COPY --from=build src/Hosts/solarLab.Api/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "solarLab.Api.dll"]
