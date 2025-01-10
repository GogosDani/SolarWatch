FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env 
WORKDIR /app
<<<<<<< HEAD
=======
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY "SolarWatch.csproj" .
RUN dotnet restore "SolarWatch.csproj"
COPY . .

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SolarWatch.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SolarWatch.dll"]

>>>>>>> main
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
<<<<<<< HEAD
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SolarWatch.dll"]
=======
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SolarWatch.dll"]

>>>>>>> main
