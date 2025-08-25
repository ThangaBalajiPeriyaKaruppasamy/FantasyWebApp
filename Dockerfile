# 1. Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY *.sln .
COPY FantasyWebApp/*.csproj ./FantasyWebApp/

# Restore dependencies for the entire solution
RUN dotnet restore FantasyWebApp.sln

# Copy the rest of your source code
COPY . .

# Publish the FantasyWebApp project
WORKDIR /src/FantasyWebApp
RUN dotnet publish -c Release -o /app/publish

# 2. Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published output from the build stage
COPY --from=build /app/publish ./

# Expose port and configure ASP.NET Core
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

# Entry point for the container
ENTRYPOINT ["dotnet", "FantasyWebApp.dll"]
