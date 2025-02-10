# Use the official .NET SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source


# Copy the project file and restore dependencies
COPY *.csproj .
RUN dotnet restore

# Copy the rest of the application code
COPY . .
# Build the ClientApp

# Build the .NET application
WORKDIR /source
RUN dotnet publish -c Release -o /app

# Use the official ASP.NET Core runtime image as the runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

# Expose the port the application runs on
EXPOSE 8080

# Set the entry point for the application
ENTRYPOINT ["dotnet", "FactApi.dll"]