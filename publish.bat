@echo off

cd src\ZKWeb.Demo.AspNetCore
dotnet build -c Release -f net461
cd ..\..

cd src\ZKWeb.Demo.Console
dotnet run -c Release -f net461
cd ..\..

..\ZKWeb\Tools\WebsitePublisher.Cmd.exe -r src\ZKWeb.Demo.AspNetCore -n "zkweb" -o "..\..\publish"
pause
