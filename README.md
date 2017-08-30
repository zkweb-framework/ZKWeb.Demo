# MPA demo based on ZKWeb framework [![Build status](https://ci.appveyor.com/api/projects/status/vjg985iy47lkvi8b?svg=true)](https://ci.appveyor.com/project/303248153/zkweb-demo)

This demo is host on [http://zkweb.org](http://zkweb.org)<br/>
Index page: [http://zkweb.org/demo](http://zkweb.org/demo)<br/>
Admin page: [http://zkweb.org/admin](http://zkweb.org/admin), account: demo, password: 123456<br/>

This demo used plugins from [ZKWeb.Plugins](https://github.com/zkweb-framework/ZKWeb.Plugins), please see the README in this project too

# How to run

- Download this project from github
- Modify 'Database' and 'ConnectionString' src\ZKWeb.Demo.AspNetCore\App_Data\config.json 
  - or you can simply replace config.json with config.sqlite.json
- Set ZKWeb.Demo.AspNetCore as primary project and run it

If you need publish this project, you will also have to download [ZKWeb](https://github.com/zkweb-framework/ZKWeb) from github<br/>
Ensure you have this diretory layout, and then run publish.bat:

- zkweb
  - tools
- zkweb.mvvmdemo
  - publish.bat

# Screenshots

![00001](screenshots/00001.jpg)
![00002](screenshots/00002.jpg)
![00003](screenshots/00003.jpg)

LICENSE: MIT LICENSE<br/>
Copyright © 2016~2017 303248153@github<br/>
If you have any license issue please contact 303248153@qq.com.<br/>

