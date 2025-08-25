# 1. Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution + project files for restore
COPY *.sln .
COPY FantasyWebApp/*.csproj ./FantasyWebApp/
COPY TestProject1/*.csproj  ./TestProject1/

# Restore all projects
RUN dotnet restore

# Copy all source, then publish the web app
COPY . .
WORKDIR /src/FantasyWebApp
RUN dotnet publish -c Release -o /app/publish

# 2. Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published output
COPY --from=build /app/publish ./

# Metadata
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

# Start the app
ENTRYPOINT ["dotnet", "FantasyWebApp.dll"]
