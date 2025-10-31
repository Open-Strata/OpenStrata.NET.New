# Contributing

Thanks for your interest in contributing to OpenStrata.

Getting started

1. Fork the repo and create a branch for your change.
2. Build and test locally (see `src/dotNet.Templates`).
3. Open a PR against `main` with a clear description.

Building templates locally

```powershell
cd src\dotNet.Templates
dotnet restore
dotnet pack -c Release /p:PushAfterBuild=false
```

Testing templates

- After pack, install the generated nupkg locally and then run `dotnet new <shortName>` to scaffold a project.

Helper scripts

- `scripts/pack-and-install.ps1` — builds, packs and installs the produced nupkg locally for testing.

Releasing

See `RELEASE.md` for instructions on creating a release and publishing to NuGet.org.

Code of conduct

Please read `CODE_OF_CONDUCT.md`.
