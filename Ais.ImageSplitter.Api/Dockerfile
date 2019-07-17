FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Ais.ImageSplitter.Api/Ais.ImageSplitter.Api.csproj", "Ais.ImageSplitter.Api/"]
RUN dotnet restore "Ais.ImageSplitter.Api/Ais.ImageSplitter.Api.csproj"
COPY . .
WORKDIR "/src/Ais.ImageSplitter.Api"
RUN dotnet build "Ais.ImageSplitter.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Ais.ImageSplitter.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Ais.ImageSplitter.Api.dll"]