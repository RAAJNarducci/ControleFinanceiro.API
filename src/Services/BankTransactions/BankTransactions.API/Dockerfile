#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/BankTransactions/BankTransactions.API/BankTransactions.API.csproj", "src/Services/BankTransactions/BankTransactions.API/"]
COPY ["src/Monitoring/Logs/Logs/Logs.csproj", "src/Monitoring/Logs/Logs/"]
COPY ["src/BuildingBlocks/EventBus/EventBusKafka/EventBusKafka/EventBusKafka.csproj", "src/BuildingBlocks/EventBus/EventBusKafka/EventBusKafka/"]
RUN dotnet restore "src/Services/BankTransactions/BankTransactions.API/BankTransactions.API.csproj"
COPY . .
WORKDIR "/src/src/Services/BankTransactions/BankTransactions.API"
RUN dotnet build "BankTransactions.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BankTransactions.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankTransactions.API.dll"]