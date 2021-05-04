 param (
    $projectName,
    $solutionRoot,
    $projects
 )
 
 .\src\bin\PackageManager.Console.exe -n $projects -o $solutionRoot -s $projectName