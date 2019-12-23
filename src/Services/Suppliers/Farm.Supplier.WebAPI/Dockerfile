#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Suppliers/Farm.Supplier.WebAPI/Farm.Supplier.WebAPI.csproj", "Services/Suppliers/Farm.Supplier.WebAPI/"]
COPY ["Services/Suppliers/Farm.Supplier.Infrastructure/Farm.Supplier.Infrastructure.csproj", "Services/Suppliers/Farm.Supplier.Infrastructure/"]
COPY ["Infrastructure/Farm.Infrastructure/Farm.Infrastructure.csproj", "Infrastructure/Farm.Infrastructure/"]
RUN dotnet restore "Services/Suppliers/Farm.Supplier.WebAPI/Farm.Supplier.WebAPI.csproj"
COPY . .
WORKDIR "/src/Services/Suppliers/Farm.Supplier.WebAPI"
RUN dotnet build "Farm.Supplier.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Farm.Supplier.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Farm.Supplier.WebAPI.dll"]