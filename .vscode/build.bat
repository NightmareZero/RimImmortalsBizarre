@echo off

REM Check if a build configuration (Debug/Release) is passed as an argument
if "%1"=="" (
    set BUILD_CONFIG=Release
) else (
    set BUILD_CONFIG=%1
)

REM output build configuration
echo Building with configuration: %BUILD_CONFIG%
echo

REM remove unnecessary assemblies
DEL /Q .\1.6\Assemblies\*.*

REM build dll with specified configuration
dotnet build .vscode -c %BUILD_CONFIG%