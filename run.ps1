 param (
    $projectName,
    $solutionRoot,
    $projects
 )
 
 .\src\bin\PackageManager.Console.exe -o $solutionRoot -s $projectName -n $projects