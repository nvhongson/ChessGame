@echo off
setlocal

rem Get the folder this launcher is running from so it works even if you
rem double-click it from Windows Explorer.
set "SCRIPT_DIR=%~dp0"

rem Store the main project file and the compiled executable path.
rem The project file is used when the .NET SDK is installed.
rem The executable is used as a fallback if the game has already been built.
set "PROJECT_FILE=%SCRIPT_DIR%ChessGame.csproj"
set "EXE_FILE=%SCRIPT_DIR%bin\Debug\net8.0\ChessGame.exe"

rem Give the console window a helpful title and show a startup message.
title Chess Game Launcher
echo Starting Chess Game...
echo.

rem Check if the dotnet command is available.
rem If it is, run the project directly so the latest source code is used.
where dotnet >nul 2>nul
if %errorlevel%==0 (
    dotnet run --project "%PROJECT_FILE%"
) else if exist "%EXE_FILE%" (
    rem If dotnet is not available, try running the last compiled executable.
    "%EXE_FILE%"
) else (
    rem If neither option is available, explain what the player needs to fix.
    echo Could not find dotnet or the compiled game executable.
    echo Install the .NET SDK or build the project in Visual Studio first.
)

rem Keep the console window open after the game ends so messages are visible.
echo.
echo Chess Game closed.
pause
