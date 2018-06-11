@echo off

rem *************************************************************************
rem Compile script for samples.
rem Microsoft Lync Server 2013, SDK
rem Copyright (c) Microsoft Corporation.  All rights reserved.
rem *************************************************************************
rem
rem Usage:
rem
rem     compile <sample>
rem
rem where <sample> is one of the sample directories.  Note that SPL samples
rem do not require compilation.
rem

rem Usage
if _%1_ == __ goto usage

rem Clear old flags
set EXTRACSCFLAGS=
set EXENAME=
set CSCFLAGS=

pushd %1

rem ServerAgent.dll reference, requiring the SDK being installed.
set SERVERAGENT="%ProgrmaFiles%\Microsoft Lync Server 2013\SDK\Bin\ServerAgent.dll"
if not exist "%SERVERAGENT%" goto NeedSDK

:SetFrameworkDir
set WHIDBEY_VER=2.0.50727
if exist "%WINDIR%\Microsoft.NET\Framework\v%WHIDBEY_VER%\csc.exe" (set FRAMEWORKDIR=%WINDIR%\Microsoft.NET\Framework\v%WHIDBEY_VER%& goto Build)

:FindCSC
rem Detect csc
if "%FRAMEWORKDIR%" == "" goto SetFrameworkDir
if not exist "%FRAMEWORKDIR%\csc.exe" goto NeedFramework
goto Build


:NeedFramework
echo The .NET Framework could not be located.
echo Please set the .NET framework path (for version %WHIDBEY_VER%) in the environment variable FRAMEWORKDIR and retry.
goto done

:Build
if not exist config.bat goto Build2
call config.bat
goto skip_config

:Build2
if not exist %1config.bat goto skip_config
call %1config.bat

:skip_config

if "%CSFILES%" == "" (echo. & echo No compilation required. & goto Success)
if "%EXENAME%" == "" (set EXENAME=%SAMPLE%)

set CSCFLAGS=/nowarn:1595 /target:exe /debug:pdbonly /out:%EXENAME%.exe %EXTRACSCFLAGS%

set CSCFLAGS=/nowarn:1595 /target:exe /debug:pdbonly /out:%SAMPLE%.exe %EXTRACSCFLAGS%
set REFERENCES=/reference:"%SERVERAGENT%","%FRAMEWORKDIR%\System.dll","%FRAMEWORKDIR%\System.Management.dll"%REFERENCES%

echo.
echo  Building %SAMPLE%...
echo.

"%FRAMEWORKDIR%\csc.exe" %CSCFLAGS% %REFERENCES% %CSFILES%
if "%ERRORLEVEL%" == "0" goto Success

:BuildFailed

echo.
echo *** Build failed ***
goto done

:Success
echo.
echo *** Build succeeded ***

:PrintHelp
echo.
echo  See %1\README.txt for instructions on running this sample.
echo.
goto done

:NeedSDK
echo %SERVERAGENT% was not found.  Please re-install the Microsoft Lync Server 2013, SDK.
echo and try again.
goto done

:usage
echo.
echo Usage: compile ^<sample^>
echo.
echo where ^<sample^> is one of the sample directories.
echo.
goto end

:done
echo.
echo Done.
set SAMPLE=
set CSFILES=
set AMFILE=
set SERVERAGENT=
set CSCFLAGS=
set REFERENCES=
popd

:end
