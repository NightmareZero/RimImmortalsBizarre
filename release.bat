@echo off
echo build with release configuration
call .vscode/build.bat


echo delete old mod file
DEL /Q /S ..\RimImmortalsBizarre-Release

echo create new mod directory
MD ..\RimImmortalsBizarre-Release

REM copy new mod files and directories
xcopy /E /I /Y .\1.5 ..\RimImmortalsBizarre-Release\1.5
xcopy /E /I /Y .\1.6 ..\RimImmortalsBizarre-Release\1.6
xcopy /E /I /Y .\About ..\RimImmortalsBizarre-Release\About
xcopy /E /I /Y .\Languages ..\RimImmortalsBizarre-Release\Languages
xcopy /E /I /Y .\Sounds ..\RimImmortalsBizarre-Release\Sounds
xcopy /E /I /Y .\Textures ..\RimImmortalsBizarre-Release\Textures


rmdir /S /Q ..\RimImmortalsBizarre-Release\1.5\obj
rmdir /S /Q ..\RimImmortalsBizarre-Release\1.5\Source
rmdir /S /Q ..\RimImmortalsBizarre-Release\1.6\obj
rmdir /S /Q ..\RimImmortalsBizarre-Release\1.6\Source
DEL /Q /S ..\RimImmortalsBizarre-Release\1.5\Assemblies\*.pdb
DEL /Q /S ..\RimImmortalsBizarre-Release\1.6\Assemblies\*.pdb
DEL /Q /S ..\RimImmortalsBizarre-Release\1.5\mod.csproj
DEL /Q /S ..\RimImmortalsBizarre-Release\1.6\mod.csproj