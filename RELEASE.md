# Release process

This document explains how to create a release and publish the template package to NuGet.org.

Prerequisites

- A GitHub repository with the `NUGET_API_KEY` secret set (Repository Settings -> Secrets).
- An account on nuget.org with an API key that has push rights.

Steps to create a release

1. Update `Directory.Packages.props` `Version` if you want an explicit version. By default package versioning uses GitInfo.
2. Create an annotated tag and push it.

```powershell
git tag -a v1.0.0 -m "Release v1.0.0"
git push origin v1.0.0
```

3. The `release` GitHub Actions workflow will run on the pushed tag. It will:
   1. Build the project
   2. Pack the template into a `.nupkg`
   3. Optionally sign (not enabled by default)
   4. Publish the nupkg to NuGet.org using `secrets.NUGET_API_KEY`

Troubleshooting

- If the release workflow fails due to missing secrets, add `NUGET_API_KEY` to the repo secrets.
- If you need to sign packages, add a base64-encoded PFX to `SIGNING_PFX_BASE64` and its password to `SIGNING_PFX_PASSWORD`.
