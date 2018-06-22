# MPA demo based on ZKWeb framework [![Build status](https://ci.appveyor.com/api/projects/status/vjg985iy47lkvi8b?svg=true)](https://ci.appveyor.com/project/303248153/zkweb-demo)

This demo is host on [http://demo.zkweb.org](http://demo.zkweb.org)<br/>
Index page: [http://demo.zkweb.org](http://demo.zkweb.org)<br/>
Admin page: [http://demo.zkweb.org/admin](http://demo.zkweb.org/admin), account: demo, password: 123456<br/>

This demo used plugins from [ZKWeb.Plugins](https://github.com/zkweb-framework/ZKWeb.Plugins), please see the README in this project too

# How to run

- Download this project from github
- Download [ZKWeb.Plugins](https://github.com/zkweb-framework/ZKWeb.Plugins) from github
- **Put ZKWeb.Demo and ZKWeb.Plugins under same directory**
- (Optional) Modify 'Database' and 'ConnectionString' in 'src\ZKWeb.Demo.AspNetCore\App_Data\config.json'
- Set ZKWeb.Demo.AspNetCore as primary project and run it

If you need publish this project, you will also have to download [ZKWeb](https://github.com/zkweb-framework/ZKWeb) from github<br/>
Ensure you have this diretory layout, and then run publish.bat:

- ZKWeb
  - Tools
- ZKWeb.Plugins
  - ZKWeb.Plugins.sln
- ZKWeb.MVVMDemo
  - publish.bat

# 如何运行

- 从github下载这个项目
- 从github下载[ZKWeb.Plugins](https://github.com/zkweb-framework/ZKWeb.Plugins)
- **把ZKWeb.Demo和ZKWeb.Plugins放在同一个文件夹下**
- (可选)修改'src\ZKWeb.Demo.AspNetCore\App_Data\config.json'中的'Database'和'ConnectionString'选项
- 设置主项目为ZKWeb.Demo.AspNetCore并运行

# Screenshots

![00001](screenshots/00001.jpg)
![00002](screenshots/00002.jpg)
![00003](screenshots/00003.jpg)

# LICENSE

LICENSE: MIT LICENSE<br/>
Copyright © 2016~2017 303248153@github<br/>
If you have any license issue please contact 303248153@qq.com.<br/>
