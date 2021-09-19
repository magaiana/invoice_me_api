#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BW.Assessment.Shop.Api/BW.Assessment.Shop.Api.csproj", "BW.Assessment.Shop.Api/"]
COPY ["BW.Assessment.Core/BW.Assessment.Core.csproj", "BW.Assessment.Core/"]
COPY ["BW.Assessment.Infrastructure/BW.Assessment.Infrastructure.csproj", "BW.Assessment.Infrastructure/"]
RUN dotnet restore "BW.Assessment.Shop.Api/BW.Assessment.Shop.Api.csproj"
COPY . .
WORKDIR "/src/BW.Assessment.Shop.Api"
RUN dotnet build "BW.Assessment.Shop.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BW.Assessment.Shop.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BW.Assessment.Shop.Api.dll"]