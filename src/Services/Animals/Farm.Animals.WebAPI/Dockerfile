#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 8002

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Animals/Farm.Animals.WebAPI/Farm.Animals.WebAPI.csproj", "Services/Animals/Farm.Animals.WebAPI/"]
COPY ["Services/Animals/Farm.Animals.Infrastructure/Farm.Animals.Infrastructure.csproj", "Services/Animals/Farm.Animals.Infrastructure/"]
COPY ["Infrastructure/Farm.Infrastructure/Farm.Infrastructure.csproj", "Infrastructure/Farm.Infrastructure/"]
RUN dotnet restore "Services/Animals/Farm.Animals.WebAPI/Farm.Animals.WebAPI.csproj"
COPY . .
WORKDIR "/src/Services/Animals/Farm.Animals.WebAPI"
RUN dotnet build "Farm.Animals.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Farm.Animals.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Farm.Animals.WebAPI.dll"]
