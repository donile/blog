#!/bin/bash

echo "Start building ..."

# Read environment specific configuration variables
source ./build/development.config

echo "Start installing ClientApp JavaScript packages ..."

pushd ./src/Blog/ClientApp

npm install

popd

echo "Finished installing ClientApp JavaScript packages ..."

echo "Start restoring .NET Core NuGet packages ..."

dotnet restore ./src/Blog/Blog.csproj --source "$nuget_source" /property:Configuration=Release

echo "Finished restoring .NET Core NuGet packages ..."

echo "Start building ClientApp ..."

# Change working directory
pushd ./src/Blog/ClientApp

npm run build

# Change working directory to repo top level directory
popd

echo "Finished building ClientApp ..."

echo "Start building .NET Core project ..."

dotnet build ./src/Blog/Blog.csproj --no-restore --configuration "$configuration" # -p:RestorePackagesPath="$nuget_packages_directory"

echo "Finished building .NET Core project ..."

echo "Finished building ..."