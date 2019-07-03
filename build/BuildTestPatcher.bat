
setlocal

set PATH=%WIX%/bin;%PATH%

call TestInstallerBuild.bat

git stash apply

call TestPatcherBuild.bat

espeak -s300 done