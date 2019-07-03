REM This script assumes you have already downloaded TeamCity dependencies

if "%FLExBridgeTestPatchVersion%"=="" set FLExBridgeTestPatchVersion=1
set /a FLExBridgeTestPatchVersion=%FLExBridgeTestPatchVersion%+1

setlocal
for /f "usebackq delims=" %%i in (`vswhere -latest -requires Microsoft.Component.MSBuild -property installationPath`) do (
	set InstallDir=%%i
)
call "%InstallDir%\VC\Auxiliary\Build\vcvars32.bat"

pushd .
(
	MSBuild FLExBridge.proj /target:RestorePackages
) && (
	MSBuild FLExBridge.proj /target:Patcher /p:Configuration=Debug /p:Platform="Any CPU" /p:BuildCounter=%FLExBridgeTestPatchVersion% %*
)
popd