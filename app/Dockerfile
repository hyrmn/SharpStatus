FROM node:lts-alpine3.14 AS node-restore
ENV NODE_ENV=production
WORKDIR /assets
COPY ["package.json", "package-lock.json", "./"]
RUN npm ci --prefer-offline --no-audit --progress=false

FROM node-restore AS node-build
COPY ["./src/SharpStatusApp/package.json", "./src/SharpStatusApp/tailwind.config.js", "./src/SharpStatusApp/"]
COPY ./src/SharpStatusApp/Styles ./src/SharpStatusApp/Styles
COPY ./src/SharpStatusApp/Scripts ./src/SharpStatusApp/Scripts
COPY ./src/SharpStatusApp/Pages ./src/SharpStatusApp/Pages
RUN npm run build --workspaces

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS base

FROM base AS dotnet-restore
WORKDIR /work
COPY SharpStatus.sln .
COPY ./src/SharpStatusApp/SharpStatusApp.csproj ./src/SharpStatusApp/
COPY ./test/SharpStatusApp.Tests/SharpStatusApp.Tests.csproj ./test/SharpStatusApp.Tests/
RUN dotnet restore -r linux-musl-x64

FROM dotnet-restore AS test
COPY ./src/ ./src
COPY ./test/ ./test
RUN dotnet build --no-restore
RUN dotnet test -r linux-musl-x64 --no-restore --no-build

FROM dotnet-restore AS publish
COPY ./src/ ./src
RUN dotnet build ./src/SharpStatusApp/SharpStatusApp.csproj -c release -o /build -r linux-musl-x64  --self-contained false --no-restore
RUN dotnet publish -c release -o /dist -r linux-musl-x64 --self-contained false --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS runtime
WORKDIR /app
COPY --from=node-build /assets/src/SharpStatusApp/wwwroot/css/site.css ./wwwroot/css/site.css
COPY --from=publish /dist .

ENV ASPNETCORE_ENVIRONMENT=production
ENV ASPNETCORE_URLS http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "SharpStatusApp.dll"]