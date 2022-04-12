@echo off
set SOURCE="C:\Users\Admin\Documents\SeriousPhysX\Output\SeriousPhysX\Plugins\SeriousPhysX.dll"
set DESTINATION="C:\Users\Admin\Documents\SeriousPhysX\Kerbal Space Program\GameData\SeriousPhysX\Plugins"
ECHO F | xcopy %SOURCE% %DESTINATION% /D /E /C /R /I /K /Y
::set SOURCE="C:\Users\Admin\Documents\SeriousPhysX\Output\"
::set DESTINATION="C:\Users\Admin\Documents\SeriousPhysX\Kerbal Space Program\GameData"
::xcopy %SOURCE% %DESTINATION% /D /E /C /R /I /K /Y
cd "Kerbal Space Program"
call KSP_x64.exe