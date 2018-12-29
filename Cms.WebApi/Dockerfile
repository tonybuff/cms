FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Cms.WebApi/Cms.WebApi.csproj", "Cms.WebApi/"]
RUN dotnet restore "Cms.WebApi/Cms.WebApi.csproj"
COPY . .
WORKDIR "/src/Cms.WebApi"
RUN dotnet build "Cms.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Cms.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Cms.WebApi.dll"]