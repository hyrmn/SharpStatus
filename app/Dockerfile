FROM debian:stable-20211011-slim AS litestream_downloader

ARG litestream_version="v0.3.8"
ARG litestream_binary_tgz_filename="litestream-${litestream_version}-linux-amd64-static.tar.gz"

WORKDIR /litestream

RUN set -x && \
    apt-get update && \
    DEBIAN_FRONTEND=noninteractive apt-get install -y \
      ca-certificates \
      wget
RUN wget "https://github.com/benbjohnson/litestream/releases/download/${litestream_version}/${litestream_binary_tgz_filename}"
RUN tar -xvzf "${litestream_binary_tgz_filename}"

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

#I have a separate restore layer so that it can be cached if there were no package changes
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

#Whereas you assume the source or tests are new each build
FROM dotnet-restore AS publish
COPY ./src/ ./src
RUN dotnet build ./src/SharpStatusApp/SharpStatusApp.csproj -c release -o /build -r linux-musl-x64  --self-contained false --no-restore
RUN dotnet publish ./src/SharpStatusApp/SharpStatusApp.csproj -c release -o /dist -r linux-musl-x64 --self-contained false --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS runtime

RUN apk add --no-cache bash

RUN mkdir -p /app/data
COPY --from=litestream_downloader /litestream/litestream /app/litestream
COPY --from=node-build /assets/src/SharpStatusApp/wwwroot/css/site.css /app/wwwroot/css/site.css
COPY --from=publish /dist /app
COPY ./litestream.yml /etc/litestream.yml
COPY ./docker_entrypoint.sh /app/docker_entrypoint.sh

RUN chmod +x /app/docker_entrypoint.sh

WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=production
ENV ASPNETCORE_URLS=http://+:8080
ENV DB_SYNC_INTERVAL=10s

EXPOSE 8080

ENTRYPOINT ["/app/docker_entrypoint.sh"]
#ENTRYPOINT ["dotnet", "SharpStatusApp.dll"]