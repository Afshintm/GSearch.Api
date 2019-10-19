FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["GSearch.Api/GSearch.Api.csproj", "GSearch.Api/"]
RUN dotnet restore "GSearch.Api/GSearch.Api.csproj"
COPY . .
WORKDIR "/src/GSearch.Api"
RUN dotnet build "GSearch.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GSearch.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GSearch.Api.dll"]