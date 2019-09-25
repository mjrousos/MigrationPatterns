@echo off

setlocal

set BinDir=%~dp0\bin\netfx
rmdir /S /Q %BinDir%
mkdir %BinDir%

cl.exe /c /clr /Zi /nologo^
 /Fo%BinDir%\\ /Fd%BinDir%\\^
 IjwLibrary.cpp

call :CheckAndLogStatus Compiling IjwLibrary

link.exe /dll /debug /machine:X64 /incremental:no /nologo^
 /out:%BinDir%\IjwLibrary.dll /pdb:%BinDir%\IjwLibrary.pdb^
 %BinDir%\IjwLibrary.obj

call :CheckAndLogStatus Linking IjwLibrary

goto :eof

:CheckAndLogStatus
IF %ERRORLEVEL% EQU 0 (
    echo [92m%*% succeeded[0m
) ELSE (
    echo [91mERROR - %*% failed[0m
    goto :eof
)
goto :eof
