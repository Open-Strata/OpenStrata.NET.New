# os-package — OpenStrata Package Template

identity: OpenStrata.NET.Package

Short name: os-package

Primary output: `Package\\OstrataTemplate.1.Package.csproj`

Description: Scaffolds a Dataverse package MSBuild project that can reference other OpenStrata template projects (ConfigData, Deployment, Solution, PowerPages, DocumentTemplates).

Parameters

- `ostrata-version`: (optional) default `1.*`

Usage example

```powershell
dotnet new os-package
```
