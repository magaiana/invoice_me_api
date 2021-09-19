#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BW.Assessment.Wallet.Api/BW.Assessment.Wallet.Api.csproj", "BW.Assessment.Wallet.Api/"]
COPY ["BW.Assessment.Core/BW.Assessment.Core.csproj", "BW.Assessment.Core/"]
COPY ["BW.Assessment.Infrastructure/BW.Assessment.Infrastructure.csproj", "BW.Assessment.Infrastructure/"]
RUN dotnet restore "BW.Assessment.Wallet.Api/BW.Assessment.Wallet.Api.csproj"
COPY . .
WORKDIR "/src/BW.Assessment.Wallet.Api"
RUN dotnet build "BW.Assessment.Wallet.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BW.Assessment.Wallet.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BW.Assessment.Wallet.Api.dll"]