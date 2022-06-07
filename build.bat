@echo off
set /p version="Enter Version Number to Build With: "

@echo on
dotnet pack ".\TomLonghurst.ReadableTimeSpan\TomLonghurst.ReadableTimeSpan.csproj"  --configuration Release /p:Version=%version%

pause