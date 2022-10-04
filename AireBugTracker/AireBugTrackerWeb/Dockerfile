#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AireBugTrackerWeb/AireBugTrackerWeb.csproj", "AireBugTrackerWeb/"]
COPY ["Repositories/Repositories.csproj", "Repositories/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["DatabaseContext/DatabaseContext.csproj", "DatabaseContext/"]
RUN dotnet restore "AireBugTrackerWeb/AireBugTrackerWeb.csproj"

COPY AireBugTrackerWeb AireBugTrackerWeb
COPY Repositories Repositories 
COPY Services Services
COPY DatabaseContext DatabaseContext

WORKDIR "/src/AireBugTrackerWeb"
RUN dotnet build "AireBugTrackerWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AireBugTrackerWeb.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AireBugTrackerWeb.dll"]