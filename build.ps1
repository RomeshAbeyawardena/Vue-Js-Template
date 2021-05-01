param (
    [string]$solutionDirectory = $PSScriptRoot,
    [string]$solutionName,
    [string]$projects
)

Write-Output $solutionDirectory
Write-Output $solutionName

if(-not (Is-Null-Or-Whitespace $solutionName)){
    Write-Output "Solution name not provided using root directory '$solutionDirectory'"
    $directory = Get-Directory $solutionDirectory

    $solutionPath = "$solutionDirectory\$directory.Name"
}

Function Get-Directory {
    Param ([string] $directoryName)

    $directory = New-Object System.IO.DirectoryInfo -ArgumentList $directoryName

   return $directory
}

Function Is-Null-Or-Whitespace {
    Param ([string] $value)
    return [string]::IsNullOrWhiteSpace($value)
}