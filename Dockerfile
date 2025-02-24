# Use the official .NET SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["HotelFlightMVC/HotelFlightMVC.csproj", "HotelFlightMVC/"]
RUN dotnet restore "HotelFlightMVC/HotelFlightMVC.csproj"

# Copy the rest of the application code and build the app
COPY . .
WORKDIR /src/HotelFlightMVC
RUN dotnet publish -c Release -o /app/publish

# Use the official ASP.NET Core runtime image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build /app/publish .

# Expose the port the app runs on
EXPOSE 80

# Run the app
ENTRYPOINT ["dotnet", "HotelFlightMVC.dll"]
