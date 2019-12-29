#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 8001

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Suppliers/Farm.Suppliers.WebAPI/Farm.Suppliers.WebAPI.csproj", "Services/Suppliers/Farm.Suppliers.WebAPI/"]
COPY ["Services/Suppliers/Farm.Suppliers.Infrastructure/Farm.Suppliers.Infrastructure.csproj", "Services/Suppliers/Farm.Suppliers.Infrastructure/"]
COPY ["Infrastructure/Farm.Infrastructure/Farm.Infrastructure.csproj", "Infrastructure/Farm.Infrastructure/"]
RUN dotnet restore "Services/Suppliers/Farm.Suppliers.WebAPI/Farm.Suppliers.WebAPI.csproj"
COPY . .
WORKDIR "/src/Services/Suppliers/Farm.Suppliers.WebAPI"
RUN dotnet build "Farm.Suppliers.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Farm.Suppliers.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Farm.Suppliers.WebAPI.dll"]
