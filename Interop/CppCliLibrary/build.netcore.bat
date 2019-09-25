@echo off

setlocal

set BinDir=%~dp0\bin\netcore
rmdir /S /Q %BinDir%
mkdir %BinDir%

cl.exe /c /clr:netcore /Zi /nologo^
 /AI"C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\3.0.0\ref\netcoreapp3.0"^
 /FU"C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\3.0.0\ref\netcoreapp3.0\System.Console.dll"^
 /Fo%BinDir%\\ /Fd%BinDir%\\^
 IjwLibrary.cpp

call :CheckAndLogStatus Compiling IjwLibrary

link.exe /dll /debug /machine:X64 /incremental:no /nologo^
 /out:%BinDir%\IjwLibrary.dll /pdb:%BinDir%\IjwLibrary.pdb^
 "C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Host.win-x64\3.0.0\runtimes\win-x64\native\IJWHOST.lib"^
 %BinDir%\IjwLibrary.obj

call :CheckAndLogStatus Linking IjwLibrary

copy /Y "C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Host.win-x64\3.0.0\runtimes\win-x64\native\IJWHOST.dll"  "%BinDir%\IJWHOST.dll"

goto :eof

:CheckAndLogStatus
IF %ERRORLEVEL% EQU 0 (
    echo [92m%*% succeeded[0m
) ELSE (
    echo [91mERROR - %*% failed[0m
    goto :eof
)
goto :eof
