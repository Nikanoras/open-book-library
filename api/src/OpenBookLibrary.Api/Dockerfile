FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY [".", "src"]
RUN dotnet restore "src/OpenBookLibrary.Api/OpenBookLibrary.Api.csproj"
COPY . .
WORKDIR "/src/src/OpenBookLibrary.Api"
RUN dotnet build "OpenBookLibrary.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OpenBookLibrary.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpenBookLibrary.Api.dll"]
