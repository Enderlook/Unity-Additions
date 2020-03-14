@echo off
echo Cleaning...
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\amd64\MSBuild.exe" "..\Enderlook.Unity\Enderlook.Unity.sln" -m /t:Clean

echo Building Editor...
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\amd64\MSBuild.exe" "..\Enderlook.Unity\Enderlook.Unity.sln" -m /t:Build /p:Configuration="%1 Editor" /p:Platform="any cpu"

echo Building Game
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\amd64\MSBuild.exe" "..\Enderlook.Unity\Enderlook.Unity.sln" -m /t:Build /p:Configuration="%1 Game" /p:Platform="any cpu"

EXIT