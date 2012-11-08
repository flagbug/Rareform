@echo on
call "%VS110COMNTOOLS%vsvars32.bat"

msbuild.exe /ToolsVersion:4.0 "..\Rareform\Rareform\Rareform.csproj" /p:configuration=Release

mkdir ..\Release

copy ..\Rareform\Rareform\bin\Release\Rareform.dll ..\Release\Rareform.dll
copy ..\Rareform\Rareform\bin\Release\Rareform.xml ..\Release\Rareform.xml

pause