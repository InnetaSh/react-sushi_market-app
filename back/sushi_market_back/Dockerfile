# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project definition files for layer caching
COPY sushi_market_back/sushi_market_back.csproj sushi_market_back/
COPY SushiMarket.BLL/SushiMarket.BLL.csproj SushiMarket.BLL/
COPY SushiMarket.DAL/SushiMarket.DAL.csproj SushiMarket.DAL/

# Restore dependencies
RUN dotnet restore sushi_market_back/sushi_market_back.csproj

# Copy project source files
COPY sushi_market_back/ sushi_market_back/
COPY SushiMarket.BLL/ SushiMarket.BLL/
COPY SushiMarket.DAL/ SushiMarket.DAL/

# Publish Release build
WORKDIR /src/sushi_market_back
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 5292
ENV ASPNETCORE_URLS=http://+:5292
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "sushi_market_back.dll"]

