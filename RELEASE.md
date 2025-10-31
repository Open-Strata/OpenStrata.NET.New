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

Signing NuGet packages

When package signing is required, the release workflow supports importing a PFX certificate and signing the produced `.nupkg` before publishing. The signing steps are optional and will only run when the required signing secrets are present.

Secrets used by the workflow

- `SIGNING_PFX_BASE64` — a base64-encoded PFX (PKCS#12) file that contains the code signing certificate and private key.
- `SIGNING_PFX_PASSWORD` — the password protecting the PFX file.
- `NUGET_API_KEY` — the API key used to push to NuGet.org (also required to publish).

Certificate requirements

- The certificate must be exportable in PFX (PKCS#12) format and include the private key.
- The certificate should be a code-signing certificate (Enhanced Key Usage: Code Signing).
- Prefer certificates that use SHA-256 (or stronger) signatures — SHA-1 certificates are discouraged.
- The certificate validity period must cover the date of signing; check expiry and plan rotation.
- The PFX must be protected with a non-empty password.
- For extra security, consider using an HSM / Azure Key Vault / other key protection; the workflow as written expects a PFX file and does not directly support external HSMs without additional changes.

How to create or export a PFX

From Windows certificate store (PowerShell example):

```powershell
# Find the certificate by subject (adjust the filter to suit your cert)
$cert = Get-ChildItem Cert:\CurrentUser\My | Where-Object { $_.Subject -like '*Your Name or Organization*' }
if (-not $cert) { Write-Error 'Certificate not found' }
# Export to PFX (you will be prompted for a password in interactive scenarios)
Export-PfxCertificate -Cert $cert -FilePath signing.pfx -Password (ConvertTo-SecureString -String 'PfxPassword' -Force -AsPlainText)
```

From PEM / private key + cert (OpenSSL example):

```bash
# Combine cert + key into a PFX (Linux/macOS shell or Windows with OpenSSL)
openssl pkcs12 -export -out signing.pfx -inkey private.key -in certificate.crt -certfile ca-chain.crt -passout pass:PfxPassword
```

Convert the PFX to base64 for storing in GitHub Secrets

```powershell
# PowerShell (recommended on Windows)
$bytes = [System.IO.File]::ReadAllBytes('signing.pfx')
$base64 = [Convert]::ToBase64String($bytes)
# Copy the string to clipboard or write to file for creating the secret in GitHub
$base64 | Set-Clipboard
```

Add the base64 string as the repository secret `SIGNING_PFX_BASE64` and the PFX password as `SIGNING_PFX_PASSWORD`.

How the workflow uses the secrets

- When `SIGNING_PFX_BASE64` is present and non-empty the workflow will:
   1. Decode the base64 string and write `signing.pfx` to the runner temporary folder.
   2. Use `dotnet nuget sign` to sign the `.nupkg` with the PFX and password, using an RFC 3161 timestamp server.
   3. If `dotnet nuget sign` is not available or fails, the workflow falls back to downloading `nuget.exe` and using `nuget sign`.

Timestamping

- The workflow uses a timestamp server (for example: `http://timestamp.digicert.com`) so signatures remain verifiable after the certificate expires. You can change the timestamper URL in the workflow if needed.

Local signing and verification

- To sign locally you can run:

```powershell
# sign a package locally (dotnet SDK must have nuget signing support)
dotnet nuget sign path\to\package.nupkg --certificate-path signing.pfx --certificate-password PfxPassword --timestamper http://timestamp.digicert.com
```

- To verify a signed package locally:

```powershell
# Using dotnet (newer SDKs) or nuget.exe verify
dotnet nuget verify path\to\package.nupkg --signature
# or
nuget.exe verify -Signatures path\to\package.nupkg
```

Security best practices

- Keep the PFX and password in GitHub Secrets, not in source control.
- Limit who can modify repository secrets and consider using organization-level secrets for shared credentials.
- Rotate code-signing certificates periodically and remove old/expired secrets.
- Consider using a dedicated signing certificate with limited scope rather than a broad-purpose certificate.

Notes and caveats

- The current release workflow only signs packages when `SIGNING_PFX_BASE64` is set. If you do not provide the signing secrets the workflow will skip signing and proceed to publish (if `NUGET_API_KEY` is set).
- If you use an HSM or cloud key vault, the workflow must be adapted to use those services (for example, by using Azure Key Vault actions or custom signing steps).

If you need help generating or importing a PFX for CI signing, tell me the environment you have (Windows, Azure Key Vault, HSM) and I can provide the exact commands or workflow snippets to integrate it.
