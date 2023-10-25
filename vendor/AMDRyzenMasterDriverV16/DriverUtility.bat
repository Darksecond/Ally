@echo OFF
set AMDRMSERVICE = AMDRyzenMasterDriverV15
set AMDRMDRIVERPATH = "%~dp0bin\AMDRyzenMasterDriver.sys"
:start
if /I "%~1" EQU "-i" goto :install
if /I "%~1" EQU "-u" goto :uninstall
if /I NOT "%~1" EQU "-u" (
IF /I NOT "%~1" EQU "-i" ( 
echo Enter Parameter "-i" to install the driver "-u" to uninstall the driver 
goto :end)
)

:uninstall
for /f "tokens=1,3 delims=: " %%a in ('sc query %AMDRMSERVICE %') do (
  if "%%a"=="STATE" set state=%%b
)

if %ERRORLEVEL% EQU 5 (
goto :accessdenied)

if %ERRORLEVEL% EQU 1060 (
goto :uninstallstatus)

if "%state%"=="STOPPED" (
goto :deleteservice)
sc stop %AMDRMSERVICE % > nul
if %ERRORLEVEL% EQU 0 (
echo Service Stopped Successfully
goto :deleteservice)

:deleteservice
sc delete %AMDRMSERVICE % > nul
if %ERRORLEVEL% EQU 0 (
goto :uninstallsuccess)
)

:uninstallstatus
echo Driver Already Uninstalled
goto :end

:uninstallsuccess
echo Driver Uninstalled Successfull
goto :end

:install
sc create %AMDRMSERVICE % binPath= %AMDRMDRIVERPATH % type= kernel start= auto> nul

if %ERRORLEVEL% EQU 5 (
goto :accessdenied)

if %ERRORLEVEL% EQU 1073 (
goto :servicestatus)

if %ERRORLEVEL% EQU 0 (
echo Service Created Successfully
goto :startservice)

:servicestatus
for /f "tokens=1,3 delims=: " %%a in ('sc query %AMDRMSERVICE %') do (
  if "%%a"=="STATE" set state=%%b
)
if "%state%"=="RUNNING" (
goto :installstatus)
if "%state%"=="STOPPED" (
goto :startservice)

:startservice
sc start %AMDRMSERVICE % > nul
if %ERRORLEVEL% EQU 0 (
goto :installsuccess)

if NOT %ERRORLEVEL% EQU 0 (
goto :error)

:installstatus
echo Driver is Already Installed
goto :end

:installsuccess
echo Driver Installed Successfully
goto :end

:error
echo Some Error in Starting Service. Make sure your Driver is Signed.
goto :end

:accessdenied
echo Error in Running the Script, Make sure you are running it with Administartive Privilages
:end