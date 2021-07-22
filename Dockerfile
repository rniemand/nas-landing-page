FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["/src/NasLandingPage/NasLandingPage.csproj", "NasLandingPage/"]
RUN dotnet restore "NasLandingPage/NasLandingPage.csproj"
COPY "/src/NasLandingPage/" "NasLandingPage/"
WORKDIR "/src/NasLandingPage"
RUN dotnet build "/src/NasLandingPage/NasLandingPage.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NasLandingPage.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NasLandingPage.dll"]