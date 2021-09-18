#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
ENV ASPNETCORE_URLS=https://+:7001;http://+:7000
WORKDIR /app
EXPOSE 7000
EXPOSE 7001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Authentication/BW.Assessment.Authentication.Api/BW.Assessment.Authentication.Api.csproj", "Authentication/BW.Assessment.Authentication.Api/"]
COPY ["Authentication/BW.Assessment.Authentication.Infrastructure/BW.Assessment.Authentication.Infrastructure.csproj", "Authentication/BW.Assessment.Authentication.Infrastructure/"]
COPY ["Authentication/BW.Assessment.Authentication.Core/BW.Assessment.Authentication.Core.csproj", "Authentication/BW.Assessment.Authentication.Core/"]
RUN dotnet restore "Authentication/BW.Assessment.Authentication.Api/BW.Assessment.Authentication.Api.csproj"
COPY . .
WORKDIR "/src/Authentication/BW.Assessment.Authentication.Api"
RUN dotnet build "BW.Assessment.Authentication.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BW.Assessment.Authentication.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BW.Assessment.Authentication.Api.dll"]
