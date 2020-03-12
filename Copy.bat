@echo off

set in=%1
set out=%2

set in=%in:"=%
set out=%out:"=%

set output=Assets\Plugins\Enderlook\Unity
set bin=Enderlook.Unity\Bin
set dlls=Dlls
set debuggers=Debbugers
set name=Enderlook.Unity
set framework=netstandard2.0
set source=%bin%\%in%\%framework%

xcopy "%source%\%name%*.dll" %output%\%dlls%\%out%\ /y

del %ouput%\%debuggers%\*

for %%i in ("%source%\%name%*.dll") do (
	echo %%i
	pdb2mdb.exe "%%i"
)
xcopy "%source%\%name%*.dll.mdb" %output%\%debuggers%\%out%\ /y
del "%source%\%name%*.dll.mdb"

exit