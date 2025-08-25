# 1. Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution + all project files needed for restore
COPY *.sln .
COPY FantasyWebApp/*.csproj ./FantasyWebApp/
COPY TestProject1/*.csproj ./TestProject1/

# restore everything (now TestProject1.csproj exists)
RUN dotnet restore

# Copy all source and publish the web app
COPY . .
WORKDIR /src/FantasyWebApp
RUN dotnet publish -c Release -o /app/publish

# 2. Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "FantasyWebApp.dll"]
