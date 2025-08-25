# 1. Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY *.sln .
COPY FantasyCricketApp/*.csproj ./FantasyCricketApp/
RUN dotnet restore

# Copy everything else and publish
COPY FantasyCricketApp/. ./FantasyCricketApp/
WORKDIR /src/FantasyCricketApp
RUN dotnet publish -c Release -o /app/publish

# 2. Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published output
COPY --from=build /app/publish ./

# Expose port and configure the app to listen on it
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

# Entrypoint to run the application
ENTRYPOINT ["dotnet", "FantasyCricketApp.dll"]
