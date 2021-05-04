 param (
    $projectName,
    $solutionRoot,
    $projects
 )
 
 .\src\bin\PackageManager.Console.exe -n $projects -o $solutionRoot -s $projectName
 
cakefrosting
console
classlib
wpf
wpflib
wpfcustomcontrollib
wpfusercontrollib
winforms
winformscontrollib
winformslib
worker
mstest
nunit
nunit-test
xunit
razorcomponent
page
viewimports
viewstart
blazorserver
blazorwasm
web
mvc
webapp
angular
react
reactredux
razorclasslib
webapi
grpc
gitignore
globaljson
nugetconfig
tool-manifest
webconfig
proto