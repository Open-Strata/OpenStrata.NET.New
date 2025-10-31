param(
    [string]$Configuration = 'Release'
)

Push-Location $PSScriptRoot\..\src\dotNet.Templates
dotnet pack -c $Configuration -o ..\..\artifacts /p:PushAfterBuild=false
$pkg = Get-ChildItem -Path ..\..\artifacts -Filter "*.nupkg" | Select-Object -First 1
if (-not $pkg) { Write-Error "No nupkg produced"; exit 1 }
dotnet new --install $pkg.FullName
Pop-Location
