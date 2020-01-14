@echo off
set url=http://repo.wyying.com/nexus/service/local/nuget/nuget-local/
set pwd=6ce45da3-9419-36d1-9630-d4b05b9c6805

echo "start WYYING.Service"
del *.nupkg
rem NuGet.exe pack
Nuget.exe Pack -Build -Prop Configuration=Release -IncludeReferencedProjects
NuGet.exe push *.nupkg  %pwd% -Source %url% 