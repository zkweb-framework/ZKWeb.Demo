@echo off
echo this script is for build and publish demo site
echo please ensure you have this directory layout
echo - zkweb
echo   - tools
echo - zkweb.demo
echo   - publish.bat
echo.

echo building project...
cd src\ZKWeb.Demo.AspNetCore
dotnet restore
dotnet build -c Release -f net461
cd ..\..

echo building plugins...
cd src\ZKWeb.Demo.Console
dotnet restore
dotnet run -c Release -f net461
cd ..\..

echo publishing website...
..\ZKWeb\Tools\WebsitePublisher.Cmd.Windows\ZKWeb.Toolkits.WebsitePublisher.Cmd.exe -r src\ZKWeb.Demo.AspNetCore -n "zkweb.demo" -o "..\..\publish"
pause
