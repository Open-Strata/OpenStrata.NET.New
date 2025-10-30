

$global:OSScriptDir = $PSScriptRoot


function global:GetSolutions
{

    $curDirName = (Get-Location | Get-Item).Name
    $curDirPath = (Get-Location).Path

    $targetSolution = "$curDirPath\$curDirName.sln"

    Show-Shortcut-Note "looking for $targetSolution"

    if ([System.IO.File]::Exists($targetSolution ))
    {
        Show-Shortcut-Note "$targetSolution"
        $targetSolution
    } 
    else
    {

        Show-Shortcut-Note "loading all Solutions"

        Get-ChildItem $PSScriptRoot\*.sln | % { $_.FullName }

    }  

}


function global:push2nuget
{
    param(
        [Parameter(Mandatory=$true)]        
        [string]$key


    )

        $solutions = GetSolutions

             foreach ($solution in $solutions){
             Show-Shortcut-Note "dotnet msbuild $solution -verbosity:normal"            
             dotnet msbuild $solution -verbosity:normal -p:NugetPushKey=$key -p:NugetPushSource=https://www.nuget.org -p:Configuration=Release
         }

}


function global:build
{

    # $curDirName = (Get-Location | Get-Item).Name
    # $curDirPath = (Get-Location).Path

    # $targetSolution = "$curDirPath\$curDirName.sln"


    # Show-Shortcut-Note "looking for $targetSolution"

    # if ([System.IO.File]::Exists($targetSolution ))
    # {
    #     Show-Shortcut-Note "$targetSolution"
    #     dotnet msbuild $targetSolution 
    # } 
    # else
    # {

    #     Show-Shortcut-Note "Building all Solutions"

    #     $solutions = Get-ChildItem $PSScriptRoot\*.sln | % { $_.FullName }
    #     foreach ($solution in $solutions){
    #         Show-Shortcut-Note "dotnet msbuild $solution"            
    #         dotnet msbuild $solution
    #     }
    # }  

        $solutions = GetSolutions

             foreach ($solution in $solutions){
             Show-Shortcut-Note "dotnet msbuild $solution"            
             dotnet msbuild $solution
         }

}

function global:restore
{

            $solutions = GetSolutions

             foreach ($solution in $solutions){
             Show-Shortcut-Note "dotnet restore $solution"            
             dotnet restore $solution 
             }   

}


function global:Show-Shortcut-Note ([string] $note) {
    Write-Host $note -ForegroundColor Black -BackgroundColor Yellow
}

$global:ostrataTerminalStartupCommand =  @"
try 
{
. "$HOME\AppData\Local\Programs\Microsoft VS Code\resources\app\out\vs\workbench\contrib\terminal\browser\media\shellIntegration.ps1"
}
catch
{}

. .\src\shortcuts.ps1

"@


function global:getvsendcodedcommand {

    $encodedcommand = [Convert]::ToBase64String([Text.Encoding]::Unicode.GetBytes($global:ostrataTerminalStartupCommand))
    $encodedcommand

}


function global:shortcuts {

    Show-Shortcut-Note ".$PSScriptRoot\shortcuts.ps1"  

    .$PSScriptRoot\shortcuts.ps1

}


function global:killdotnet {
    $dotnetProcesses = Get-Process -Name "dotnet" -ErrorAction SilentlyContinue
     
    if ($dotnetProcesses) {
        Write-Host "Stopping all 'dotnet' processes..."
        $dotnetProcesses | Stop-Process
        Write-Host "All 'dotnet' processes have been stopped."
    } else {
        Write-Host "No 'dotnet' processes found."
    }
    }