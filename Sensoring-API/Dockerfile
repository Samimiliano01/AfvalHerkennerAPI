# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY Sensoring-API/*.csproj ./Sensoring-API/
RUN dotnet restore Sensoring-API/Sensoring-API.csproj
COPY . .
WORKDIR /src/Sensoring-API
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Use the runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Sensoring-API.dll"]
