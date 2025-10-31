# Contributing

Thanks for your interest in contributing to OpenStrata.

Getting started

1. Fork the repo and create a branch for your change.
2. Build and test locally (see `src/dotNet.Templates`).
3. Open a PR against `main` with a clear description and tests where appropriate.

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

We keep detailed release and publishing steps in `RELEASE.md`. In short:

- Create an annotated tag (for example `v1.0.0`) and push it to trigger the release workflow.
- The workflow will build and pack the templates and publish the resulting `.nupkg` to NuGet.org if the `NUGET_API_KEY` repository secret is configured.
- If you require signed packages, add the `SIGNING_PFX_BASE64` and `SIGNING_PFX_PASSWORD` secrets as described in `RELEASE.md`.

See `RELEASE.md` for full details, commands, and signing instructions.

Code of conduct

Please read `CODE_OF_CONDUCT.md`.
