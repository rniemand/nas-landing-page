# https://mcr.microsoft.com/en-us/product/dotnet/aspnet/tags
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Setup NPM
# SEE: https://github.com/nodesource/distributions
RUN apt-get update
RUN apt-get install -y ca-certificates curl gnupg
RUN mkdir -p /etc/apt/keyrings
RUN curl -fsSL https://deb.nodesource.com/gpgkey/nodesource-repo.gpg.key | gpg --dearmor -o /etc/apt/keyrings/nodesource.gpg
RUN echo "deb [signed-by=/etc/apt/keyrings/nodesource.gpg] https://deb.nodesource.com/node_21.x nodistro main" | tee /etc/apt/sources.list.d/nodesource.list
RUN apt-get update
RUN apt-get install nodejs -y

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
