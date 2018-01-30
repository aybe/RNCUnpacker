if exist binaries.zip del binaries.zip 
if exist .bin rd /s /q .bin
if exist .obj rd /s /q .obj
MSBuild /t:Rebuild /p:Configuration=Release /p:Platform=x86
MSBuild /t:Rebuild /p:Configuration=Release /p:Platform=x64
7z a binaries.zip .bin

if exist binaries_unity.zip del binaries_unity.zip
if exist .bin rd /s /q .bin
if exist .obj rd /s /q .obj
MSBuild /t:Rebuild /p:Configuration=Release /p:Platform=x86 /p:DefineConstants=RNC_UNITY
MSBuild /t:Rebuild /p:Configuration=Release /p:Platform=x64 /p:DefineConstants=RNC_UNITY
7z a binaries_unity.zip .bin

rd /s /q .bin
rd /s /q .obj
