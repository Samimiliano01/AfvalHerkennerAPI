FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Sensoring-API-Test/Sensoring-API-Test.csproj", "Sensoring-API-Test/"]
RUN dotnet restore "Sensoring-API-Test/Sensoring-API-Test.csproj"
COPY . .
WORKDIR "/src/Sensoring-API-Test"
RUN dotnet build "Sensoring-API-Test.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Sensoring-API-Test.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sensoring-API-Test.dll"]
