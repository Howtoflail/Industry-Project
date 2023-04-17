# ============= #
# === Build === #
# ============= #
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /KindRegardsApi

COPY . ./
ADD ./KindRegardsApi.Host/appsettings.Docker.Dev.json ./KindRegardsApi.Host/appsettings.json
RUN dotnet restore

RUN dotnet publish -c Release -o dist

# ================== #
# === Production === #
# ================== #
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as production
WORKDIR /KindRegardsApi

COPY --from=build /KindRegardsApi/dist .

ENTRYPOINT ["dotnet", "KindRegardsApi.Host.dll"]

