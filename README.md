# OpenStrata.NET.New

![CI](https://github.com/Open-Strata/OpenStrata.NET.New/actions/workflows/ci.yml/badge.svg)
![Release](https://github.com/Open-Strata/OpenStrata.NET.New/actions/workflows/release.yml/badge.svg)


OpenStrata.NET.New packages a set of `dotnet new` templates that scaffold production-ready MSBuild projects and solutions for the OpenStrata initiative (Power Platform / Dataverse).

This repository contains a template pack project (`src/dotNet.Templates`) which, when built, produces a NuGet template package that exposes multiple templates (for example `os-dotnet`, `os-package`, `os-plugin`, `os-strati`, and more) to create solutions and projects tailored for Dataverse/PowerPlatform development.

Quick links
- docs/TEMPLATES.md  documentation for each template (parameters, outputs, usage examples)
- src/dotNet.Templates  main template pack project; build with `dotnet pack` to produce the nupkg

Getting started (local)

1. Build and pack the template (from repository root):

```powershell
cd src\dotNet.Templates
dotnet pack -c Release /p:PushAfterBuild=false
```

2. Install the packed template locally (replace the filename with the produced nupkg):

```powershell
# adjust filename as appropriate
dotnet new --install .\src\dotNet.Templates\bin\Release\OpenStrata.NET.Templates.1.0.30.nupkg
```

3. Create a new OpenStrata solution using the main solution template:

```powershell
dotnet new os-dotnet -pn "Your Publisher" -pp "yourns"
```

See `docs/TEMPLATES.md` for a full list of available templates and individual usage notes.

License and contributing

See `src/dotNet.Templates/LICENSE.txt` and this repository's `license.txt` for licensing information.
