#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/EventService.Api/EventService.Api.csproj", "src/EventService.Api/"]
COPY ["src/EventService.Application/EventService.Application.csproj", "src/EventService.Application/"]
COPY ["SC.Internship.Common/SC.Internship.Common/SC.Internship.Common.csproj", "SC.Internship.Common/SC.Internship.Common/"]
COPY ["src/EventService.Domain/EventService.Domain.csproj", "src/EventService.Domain/"]
COPY ["src/EventService.Contracts/EventService.Contracts.csproj", "src/EventService.Contracts/"]
COPY ["src/EventService.Infrastructure/EventService.Infrastructure.csproj", "src/EventService.Infrastructure/"]
RUN dotnet restore "src/EventService.Api/EventService.Api.csproj"
COPY . .
WORKDIR "/src/src/EventService.Api"
RUN dotnet build "EventService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventService.Api.dll"]