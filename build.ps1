Remove-Item .\bin -Recurse
dotnet publish .\src\PackageManager.Console\PackageManager.Console.csproj -o .\bin --nologo --self-contained --runtime win10-x64