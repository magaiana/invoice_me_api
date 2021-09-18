#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
ENV ASPNETCORE_URLS=https://+:8001;http://+:8000
WORKDIR /app
EXPOSE 8000
EXPOSE 8001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Wallet/BW.Assessment.Wallet.Api/BW.Wallet.Wallet.Api.csproj", "Wallet/BW.Assessment.Wallet.Api/"]
COPY ["Wallet/BW.Assessment.Wallet.Core/BW.Wallet.Authentication.Core.csproj", "Wallet/BW.Assessment.Wallet.Core/"]
COPY ["Wallet/BW.Assessment.Wallet.Infrastructure/BW.Wallet.Authentication.Infrastructure.csproj", "Wallet/BW.Assessment.Wallet.Infrastructure/"]
RUN dotnet restore "Wallet/BW.Assessment.Wallet.Api/BW.Wallet.Wallet.Api.csproj"
COPY . .
WORKDIR "/src/Wallet/BW.Assessment.Wallet.Api"
RUN dotnet build "BW.Wallet.Wallet.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BW.Wallet.Wallet.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BW.Wallet.Wallet.Api.dll"]