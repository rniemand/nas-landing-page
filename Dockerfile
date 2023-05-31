FROM mcr.microsoft.com/dotnet/aspnet:7.0-bullseye-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0-bullseye-slim AS build

# Setup NPM
RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs

WORKDIR /src
COPY ["/src/NasLandingPage/NasLandingPage.csproj", "NasLandingPage/"]
RUN dotnet restore "NasLandingPage/NasLandingPage.csproj"
COPY "/src/" "/src/"
COPY "/ui/" "/ui/"
WORKDIR "/src/NasLandingPage"
RUN dotnet build "NasLandingPage.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NasLandingPage.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NasLandingPage.dll"]
