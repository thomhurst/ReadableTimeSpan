@echo off
set /p version="Enter Version Number to Build With: "

@echo on
dotnet pack ".\TomLonghurst.ReadableTimeSpan\TomLonghurst.ReadableTimeSpan.csproj"  --configuration Release /p:Version=%version%

dotnet pack ".\TomLonghurst.ReadableTimeSpan.Newtonsoft.Json\TomLonghurst.ReadableTimeSpan.Newtonsoft.Json.csproj"  --configuration Release /p:Version=%version%

dotnet pack ".\TomLonghurst.ReadableTimeSpan.System.Text.Json\TomLonghurst.ReadableTimeSpan.System.Text.Json.csproj"  --configuration Release /p:Version=%version%

pause