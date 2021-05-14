 param (
    $projectName,
    $solutionRoot,
    $projects
 )
 
 Invoke-Expression "$PSScriptRoot\src\bin\PackageManager.Console.exe -o $solutionRoot -s $projectName -n $projects" 