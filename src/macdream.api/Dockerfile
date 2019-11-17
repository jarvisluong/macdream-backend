FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["macdream.api/macdream.api.csproj", "macdream.api/"]
RUN dotnet restore "macdream.api/macdream.api.csproj"
COPY . .
WORKDIR "/src/macdream.api"
RUN dotnet build "macdream.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "macdream.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Heroku expects CMD and not ENTRYPOINT
CMD ["dotnet", "macdream.api.dll"]
