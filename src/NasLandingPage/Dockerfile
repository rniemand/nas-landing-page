# https://docs.microsoft.com/en-us/visualstudio/containers/container-tools-react?view=vs-2022
# https://github.com/microsoft/containerregistry/blob/main/docs/dockerhub-to-mcr-repo-mapping.md
# https://hub.docker.com/_/microsoft-dotnet-sdk/
# https://hub.docker.com/_/microsoft-dotnet-aspnet/

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs
WORKDIR /src
COPY ["NasLandingPage/NasLandingPage.csproj", "NasLandingPage/"]
RUN dotnet restore "NasLandingPage/NasLandingPage.csproj"
COPY . .
WORKDIR "/src/NasLandingPage"
RUN dotnet build "NasLandingPage.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NasLandingPage.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NasLandingPage.dll"]