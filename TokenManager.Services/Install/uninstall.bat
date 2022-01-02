@ECHO OFF
set DOTNETFX4=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
set PATH=%PATH%;%DOTNETFX4%
echo uninstalling TokenManager.Services.exe
echo ---------------------------------------------------
InstallUtil /u C:\Project\Interview\TokenManager.IO\TokenManager.Services\Install\TokenManager.Services.exe
echo ---------------------------------------------------
echo Done.
pause