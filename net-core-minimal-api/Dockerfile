# --- Stage 1: Build & Publish ---
# Uses the full .NET 10 SDK to restore and compile the app
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy only the project file first to leverage Docker's cache layer for dependencies
COPY *.csproj ./
RUN dotnet restore

# Dynamic build configuration argument (defaults to Release)
ARG BUILD_CONFIGURATION=Release

# Copy the rest of your application code and build it
COPY . ./
RUN dotnet publish -c ${BUILD_CONFIGURATION} -o /app/publish /p:UseAppHost=false

# --- Stage 2: Final Runtime ---
# Uses the ultra-lean ASP.NET Core 10 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Expose both standard HTTP and secure HTTPS ports
EXPOSE 8080
EXPOSE 8081

# Copy the compiled binaries from the build stage
COPY --from=build /app/publish .

# Run the application
ENTRYPOINT ["dotnet", "net-core-minimal-api.dll"]