# Publishing packages (manual workflow)

This repository uses a manual GitHub Actions workflow to publish the built NuGet packages to nuget.org. The workflow is named `Publish` and is defined in `.github/workflows/publish.yml`.

Why manual?

- Publishing requires a live NuGet API key and we avoid accidental publishes from tag pushes. The manual workflow lets maintainers run a controlled publish and optionally override defaults.

Workflow inputs

- `NugetPushKey` (optional): The NuGet API key. If omitted, the workflow will use the repository secret `NUGET_API_KEY` (if present).

- `NugetPushSource` (optional): The NuGet push source URL. Defaults to `https://api.nuget.org/v3/index.json`.

How it works

1. The workflow reads the `NugetPushKey` input. If it's empty it falls back to the repository secret `secrets.NUGET_API_KEY`.

2. If no key is available the workflow exits early and prints a message.

3. When a key is available the workflow runs the MSBuild `PushPackage` target on `src/dotNet.Templates/dotNet.Templates.csproj`:

   dotnet msbuild src\dotNet.Templates\dotNet.Templates.csproj -t:PushPackage -p:NugetPushKey=`<key>` -p:NugetPushSource=`<source>` -p:Configuration=Release

Running the workflow from the GitHub UI

1. Navigate to the repository Actions tab.

2. Select the `Publish` workflow.

3. Click "Run workflow".

4. Provide `NugetPushKey` (or leave blank to use the `NUGET_API_KEY` secret) and optionally set `NugetPushSource`.

Running the workflow with the GitHub CLI
From PowerShell, you can run the workflow and pass inputs:

```powershell
gh workflow run publish.yml -f NugetPushKey="$env:NUGET_API_KEY" -f NugetPushSource="https://api.nuget.org/v3/index.json"
```

Or run it without inputs (it will use the repository secret if set):

```powershell
gh workflow run publish.yml
```

Security note

- Store your NuGet API key as repository secret `NUGET_API_KEY` (recommended). Only allow trusted maintainers to run publish.
