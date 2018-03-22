#!/usr/bin/env bash
set -e

echo this script is for build and publish demo site
echo please ensure you have this directory layout
echo "- ZKWeb"
echo "  - tools"
echo "- ZKWeb.demo"
echo "  - publish.sh"
echo

echo building project...
cd src/ZKWeb.Demo.AspNetCore
dotnet restore
dotnet build -c Release -f netcoreapp2.0
dotnet publish -c Release -f netcoreapp2.0 -r ubuntu.16.04-x64
cd ../..

echo building plugins...
cd src/ZKWeb.Demo.Console
dotnet restore
dotnet run -c Release -f netcoreapp2.0
cd ../..

echo publishing website...
dotnet ../ZKWeb/Tools/WebsitePublisher.Cmd.NetCore/ZKWeb.Toolkits.WebsitePublisher.Cmd.dll -f netcoreapp2.0 -r src/ZKWeb.Demo.AspNetCore -n "zkweb.demo" -o "../ZKWeb.Demo.Publish"

