rd /s /q .bin
rd /s /q .obj
MSBuild /t:Rebuild /p:Configuration=Release /p:Platform=x86 /p:DefineConstants=RNC_UNITY
MSBuild /t:Rebuild /p:Configuration=Release /p:Platform=x64 /p:DefineConstants=RNC_UNITY
