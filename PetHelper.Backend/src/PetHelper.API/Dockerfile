#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/PetHelper.API/PetHelper.API.csproj", "src/PetHelper.API/"]
COPY ["src/PetHelper.Application/PetHelper.Application.csproj", "src/PetHelper.Application/"]
COPY ["src/PetHelper.Domain/PetHelper.Domain.csproj", "src/PetHelper.Domain/"]
COPY ["src/PetHelper.Infastructure/PetHelper.Infastructure.csproj", "src/PetHelper.Infastructure/"]
RUN dotnet restore "./src/PetHelper.API/PetHelper.API.csproj"
COPY . .
WORKDIR "/src/src/PetHelper.API"
RUN dotnet build "./PetHelper.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PetHelper.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetHelper.API.dll"]