FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/OpenBookLibrary.WebApi/OpenBookLibrary.WebApi.csproj", "src/OpenBookLibrary.WebApi/"]
RUN dotnet restore "src/OpenBookLibrary.WebApi/OpenBookLibrary.WebApi.csproj"
COPY . .
WORKDIR "/src/src/OpenBookLibrary.WebApi"
RUN dotnet build "OpenBookLibrary.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OpenBookLibrary.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpenBookLibrary.WebApi.dll"]
