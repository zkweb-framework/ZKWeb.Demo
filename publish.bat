@echo off

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
..\ZKWeb\Tools\WebsitePublisher.Cmd.exe -r src\ZKWeb.Demo.AspNetCore -n "zkweb" -o "..\..\publish"
pause
