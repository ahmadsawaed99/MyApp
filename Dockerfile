# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# copy only the csproj first (better layer caching)
COPY Backend/Backend.csproj Backend/
RUN dotnet restore Backend/Backend.csproj

# now copy the rest of the source
COPY Backend/ Backend/

# publish (no app host to keep image smaller)
RUN dotnet publish Backend/Backend.csproj -c Release -o /app/publish /p:UseAppHost=false

# ---- Runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Backend.dll"]
