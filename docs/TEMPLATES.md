# OpenStrata Templates

This repository contains a package of `dotnet new` templates for OpenStrata. Build and install the `src/dotNet.Templates` package to expose the templates locally. See the individual pages in `docs/templates/` for details.

Available templates (shortName)

- os-dotnet — OpenStrata .Net MSBuild Solution Template (main solution template)
- os-package — OpenStrata Package Template
- os-plugin — OpenStrata Plugin Template
- os-strati — OpenStrata Strati Template
- os-deployment — OpenStrata Deployment Template
- os-configdata — OpenStrata ConfigData Template
- os-solution — Dataverse Solution Template
- os-powerpages — OpenStrata PowerPages Template
- os-pcf — OpenStrata PCF Template
- os-doctemplates — OpenStrata Document Templates Project
- os-essentials — OpenStrata Essentials
- os-props — OpenStrata Props File
- os-strataversions — OpenStrata Strata-Versions File

Quick usage examples

Install locally (after packing):

```powershell
dotnet new --install .\src\dotNet.Templates\bin\Release\OpenStrata.NET.Templates.*.nupkg
```

Create a new solution from the main template:

```powershell
dotnet new os-dotnet -pn "Your Publisher" -pp "yourns"
```

Per-template docs live in `docs/templates/`.
