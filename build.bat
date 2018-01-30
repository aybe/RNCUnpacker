rd /s /q .bin
rd /s /q .obj
MSBuild /t:Rebuild /p:Configuration=Release /p:Platform=x86
MSBuild /t:Rebuild /p:Configuration=Release /p:Platform=x64
