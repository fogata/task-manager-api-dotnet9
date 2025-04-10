FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["TaskManager.Api/TaskManager.Api.csproj", "TaskManager.Api/"]
COPY ["TaskManager.Application/TaskManager.Application.csproj", "TaskManager.Application/"]
COPY ["TaskManager.Core/TaskManager.Core.csproj", "TaskManager.Core/"]
COPY ["TaskManager.Infrastructure/TaskManager.Infrastructure.csproj", "TaskManager.Infrastructure/"]

RUN dotnet restore "TaskManager.Api/TaskManager.Api.csproj"
COPY . .
WORKDIR "/src/TaskManager.Api"
RUN dotnet build "TaskManager.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskManager.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
RUN apt-get update && apt-get install -y netcat-traditional
COPY --from=publish /app/publish .
COPY TaskManager.Api/wait-for-postgres.sh .
RUN chmod +x ./wait-for-postgres.sh
ENTRYPOINT ["./wait-for-postgres.sh", "db", "dotnet", "TaskManager.Api.dll"]
