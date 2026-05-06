# Get the folder this launcher is running from so relative paths stay correct.
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path

# Store the main project file and compiled executable path.
# The project file is used when the .NET SDK is installed.
# The executable is used as a fallback if the game has already been built.
$projectFile = Join-Path $scriptDir "ChessGame.csproj"
$exeFile = Join-Path $scriptDir "bin\Debug\net8.0\ChessGame.exe"

# Show a simple startup message before launching the game.
Write-Host "Starting Chess Game..."
Write-Host ""

# Prefer dotnet run because it starts the game from the current source code.
if (Get-Command dotnet -ErrorAction SilentlyContinue) {
    dotnet run --project $projectFile
}
elseif (Test-Path $exeFile) {
    # If dotnet is unavailable, try running the last compiled executable.
    & $exeFile
}
else {
    # If neither option is available, explain what the player needs to install.
    Write-Host "Could not find dotnet or the compiled game executable."
    Write-Host "Install the .NET SDK or build the project in Visual Studio first."
}

# Keep the PowerShell window open after the game ends so messages are visible.
Write-Host ""
Write-Host "Chess Game closed."
Read-Host "Press Enter to exit"
