# os-powerpages — OpenStrata PowerPages Template

identity: OpenStrata.NET.PowerPages

Short name: os-powerpages

Primary output: `PowerPages\\OstrataTemplate.1.PowerPages.csproj`

Parameters

- `website-id` (required): WebSiteId unique to portal config
- `website-folder` (required): Folder to place the website content
- `ostrata-version` (optional)

Usage example

```powershell
dotnet new os-powerpages -website-id "GUID" -website-folder "MyPortal"
```
