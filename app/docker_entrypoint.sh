#!/bin/bash

is_litestream_enabled() {
  set +ux

  local IS_ENABLED='false'

  if [[ ! -z "${DB_REPLICA_URL}" ]]; then
    IS_ENABLED='true';
  fi

  set -ux

  echo "${IS_ENABLED}"
}

readonly IS_LITESTREAM_ENABLED="$(is_litestream_enabled)"

set -x

if [[ "${IS_LITESTREAM_ENABLED}" == 'true' ]]; then

  /app/litestream restore -if-replica-exists

  exec /app/litestream replicate \
    -exec "dotnet SharpStatusApp.dll"
else
  # Start server.
  dotnet SharpStatusApp.dll
fi