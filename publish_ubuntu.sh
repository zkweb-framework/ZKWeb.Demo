#!/usr/bin/env bash
set -e

if [ ! -f "/usr/lib/gdiplus.dll" ]; then
    echo "ERROR:"
    echo "please install libgdiplus first:"
    echo "================================"
    echo "sudo apt-get install libgdiplus"
    echo "cd /usr/lib"
    echo "ln -s libgdiplus.so gdiplus.dll"
    echo "================================"
    exit
fi

if [ ! -d "../ZKWeb/Tools" ]; then
    echo "ERROR:"
    echo "please download ZKWeb and put it in the same directory as ZKWeb.Demo"
    exit
fi

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
dotnet ../ZKWeb/Tools/WebsitePublisher.Cmd.NetCore/ZKWeb.Toolkits.WebsitePublisher.Cmd.dll -f netcoreapp2.0 -r src/ZKWeb.Demo.AspNetCore -n "ZKWeb.Demo.Ubuntu" -o "../ZKWeb.Demo.Publish"
echo "output directory: ../ZKWeb.Demo.Publish"

echo "BUG FIX:"
echo "this is a workaround to fix #24832 on corefx, please ensure you installed .net core 2.0.6"
echo "you can check the version from 'dotnet --info'"
echo "if you using a higher version like 2.0.7, please replace the version in this script"
cp -f /usr/share/dotnet/shared/Microsoft.NETCore.App/2.0.6/* ../ZKWeb.Demo.Publish/ZKWeb.Demo.Ubuntu
