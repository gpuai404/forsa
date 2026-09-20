#!/usr/bin/env bash

set -euo pipefail

cd "$(dirname "${BASH_SOURCE[0]}")/.."

readonly application_tests="tests/Forsa.Application.Tests/Forsa.Application.Tests.csproj"
readonly infrastructure_tests="tests/Forsa.Infrastructure.Tests/Forsa.Infrastructure.Tests.csproj"

dotnet restore "$application_tests" --locked-mode
dotnet restore "$infrastructure_tests" --locked-mode

dotnet test "$application_tests" --configuration Release --no-restore
dotnet test "$infrastructure_tests" --configuration Release --no-restore