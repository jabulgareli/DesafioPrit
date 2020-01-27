FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/Prit.Portal/Prit.Portal.csproj", "src/Prit.Portal/"]
RUN dotnet restore "src/Prit.Portal/Prit.Portal.csproj"
COPY . .
WORKDIR "/src/src/Prit.Portal"
RUN dotnet build "Prit.Portal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Prit.Portal.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Prit.Portal.dll"]