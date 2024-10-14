# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copy the development certificates
COPY /path/to/yourcert.pfx /home/app/.aspnet/https/yourcert.pfx

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ToDoList.Api/ToDoList.Api.csproj", "src/ToDoList.Api/"]
COPY ["src/ToDoList.Application/ToDoList.Application.csproj", "src/ToDoList.Application/"]
COPY ["src/ToDoList.Core/ToDoList.Core.csproj", "src/ToDoList.Core/"]
COPY ["src/ToDoList.Infrastructure/ToDoList.Infrastructure.csproj", "src/ToDoList.Infrastructure/"]
COPY ["src/ToDoList.Persistence/ToDoList.Persistence.csproj", "src/ToDoList.Persistence/"]
RUN dotnet restore "./src/ToDoList.Api/ToDoList.Api.csproj"
COPY . .
WORKDIR "/src/src/ToDoList.Api"
RUN dotnet build "./ToDoList.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ToDoList.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDoList.Api.dll"]