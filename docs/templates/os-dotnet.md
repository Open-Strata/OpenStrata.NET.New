# os-dotnet — OpenStrata .Net MSBuild Solution Template

identity: OpenStrata.NET.DotNetSolution

Short name: os-dotnet

Description: Produces a multi-project MSBuild solution containing OpenStrata project types (Strati, Package, Plugin, Deployment, ConfigData, etc.).

Parameters

- `publisher-name` (required): Name of the Dataverse solution publisher
- `publisher-prefix` (required): Customization prefix value for the Dataverse solution publisher
- `ostrata-version`: (optional) default `1.*` — replaces `ostrataVersion.1`
- `noext` (bool): if false, template will attempt post-actions to add more extensions (default: false)

Primary output

- `OstrataTemplate.1.sln`

Post-actions

- installs VSCode extension templates and runs `dotnet restore` when `noext == false`.

Usage example

```powershell
# create a new OpenStrata solution
dotnet new os-dotnet -pn "Contoso Publisher" -pp "contoso"
```
