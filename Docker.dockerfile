# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /src

# Copy the .csproj and restore any dependencies (via NuGet)
COPY ["HotelFlightMVC/HotelFlightMVC.csproj", "HotelFlightMVC/"]
RUN dotnet restore "HotelFlightMVC/HotelFlightMVC.csproj"

# Copy the entire project and publish it to /app
COPY . .
WORKDIR "/src/HotelFlightMVC"
RUN dotnet publish "HotelFlightMVC.csproj" -c Release -o /app/publish

# Use the official .NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Set the working directory inside the container
WORKDIR /app

# Copy the published app from the build stage
COPY --from=build /app/publish .

# Expose port 80 to be able to access the application
EXPOSE 80

# Set the entry point for the container to run the app
ENTRYPOINT ["dotnet", "HotelFlightMVC.dll"]
