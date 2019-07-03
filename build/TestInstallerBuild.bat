REM This script assumes you have already downloaded TeamCity dependencies

setlocal
for /f "usebackq delims=" %%i in (`vswhere -latest -requires Microsoft.Component.MSBuild -property installationPath`) do (
	set InstallDir=%%i
)
call "%InstallDir%\VC\Auxiliary\Build\vcvars32.bat"

set Path=%WIX%\bin;%PATH%

pushd .
(
	MSBuild FLExBridge.proj /target:RestorePackages
) && (
	MSBuild FLExBridge.proj /target:Installer /p:Configuration=Debug /p:Platform="Any CPU" %*
)
popd