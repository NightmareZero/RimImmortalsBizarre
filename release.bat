@echo off
echo build with release configuration
call .vscode/build.bat


echo delete old mod file
DEL /Q /S ..\RimImmortalsBizarre-Release

echo create new mod directory
MD ..\RimImmortalsBizarre-Release

REM copy new mod files and directories
xcopy /E /I /Y .\1.5 ..\RimImmortalsBizarre-Release\
xcopy /E /I /Y .\About ..\RimImmortalsBizarre-Release\
xcopy /E /I /Y .\Sounds ..\RimImmortalsBizarre-Release\
xcopy /E /I /Y .\Textures ..\RimImmortalsBizarre-Release\