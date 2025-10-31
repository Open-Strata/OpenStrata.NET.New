# Remove any .snk files under the repo (useful before public release)
Get-ChildItem -Path "$PSScriptRoot\..\src" -Recurse -Filter "*.snk" -File | ForEach-Object {
    Write-Host "Removing $($_.FullName)"
    Remove-Item -Path $_.FullName -Force
}
