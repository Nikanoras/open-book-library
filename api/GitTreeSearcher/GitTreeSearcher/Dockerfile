FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GitTreeSearcher/GitTreeSearcher.csproj", "GitTreeSearcher/"]
RUN dotnet restore "GitTreeSearcher/GitTreeSearcher.csproj"
COPY . .
WORKDIR "/src/GitTreeSearcher"
RUN dotnet build "GitTreeSearcher.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GitTreeSearcher.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GitTreeSearcher.dll"]
