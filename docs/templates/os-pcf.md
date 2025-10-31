# os-pcf — OpenStrata PCF Template

identity: OpenStrata.NET.PCF

Short name: os-pcf

Description: Initializes a PCF subdirectory with a Power Apps component framework project extended with OpenStrata. The template supports `field` or `dataset` templates and `none` or `react` frameworks.

Parameters

- `namespace` (required)
- `template` (choice): `field` (default) or `dataset`
- `framework` (choice): `none` (default) or `react`
- `run-npm-install` (bool): whether to run npm install automatically

Primary output: `PCF/PCFProjectSubFolder/OstrataTemplate.1.pcfproj`

Notes: When `run-npm-install` is true the template runs a PowerShell script to install npm packages in the created subfolder.

Usage example

```powershell
dotnet new os-pcf -namespace "Contoso.Controls" -template field -framework react -run-npm-install true
```
