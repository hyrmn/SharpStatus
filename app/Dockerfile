#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/SharpStatusApp/SharpStatusApp.csproj", "SharpStatusApp/"]
RUN dotnet restore "SharpStatusApp/SharpStatusApp.csproj"
COPY ./src .
RUN dotnet build "SharpStatusApp/SharpStatusApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SharpStatusApp/SharpStatusApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_ENVIRONMENT=production
ENV ASPNETCORE_URLS http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "SharpStatusApp.dll"]