#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Hosts/solarLab.Migrations/solarLab.Migrations.csproj", "Hosts/solarLab.Migrations/"]
RUN dotnet restore "Hosts/solarLab.Migrations/solarLab.Migrations.csproj"
COPY . .
WORKDIR "/src/Hosts/solarLab.Migrations"
RUN dotnet build "solarLab.Migrations.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "solarLab.Migrations.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "solarLab.Migrations.dll"]