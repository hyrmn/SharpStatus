#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/SharpStatus.csproj", "src/"]
RUN dotnet restore "src/SharpStatus.csproj"
COPY ./src .
RUN dotnet build "SharpStatus.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SharpStatus.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_ENVIRONMENT=production
ENV ASPNETCORE_URLS http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "SharpStatus.dll"]