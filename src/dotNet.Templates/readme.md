**OpenStrata.NET.Templates** is a .Net Core Template package containing templates for the various OpenStrata project types and an OpenStrata Solution template containing each of the OpenStrata project types with project references and dependencies pre set.

To get started, install the templates using the dotnet CLI new command as seen below.

```
dotnet new --install OpenStrata.NET.Templates
```
Next, create a folder in which you want to create  a new MSBuild Project/ Visual Studio Solution.

We recommend using the `openstrata-dotnetsolution` template to get started.

From the directory just created, run the following command.

```
dotnet new openstrata-dotnetsolution
```
  ⚠  
*By default, the folder name will be used as the solution name unless --name argument is used.  If you prefer a  different name than the folder name, then use the following command, replacing preferred name with your preference.*

```
dotnet new openstrata-dotnetsolution --name [preferred-name]
```
  ⚠  
*As a best practice, we recommend one dotNet solution
 per code repository.*


 For additonal insight into each OpenStrata project type:  

- [Strati Project](https://www.nuget.org/packages/OpenStrata.MSBuild.Stratify)  
- [Package Project](https://www.nuget.org/packages/OpenStrata.MSBuild.Package)
- [Deployment Project](https://www.nuget.org/packages/OpenStrata.MSBuild.Deployment)
- [AppSource Project](https://www.nuget.org/packages/OpenStrata.MSBuild.AppSource)
- [ConfigData Project](https://www.nuget.org/packages/OpenStrata.MSBuild.ConfigData)
- [Solution Project](https://www.nuget.org/packages/OpenStrata.MSBuild.Solution)
- [Plugin Project](https://www.nuget.org/packages/OpenStrata.MSBuild.Plugin)
- [PCF Project](https://www.nuget.org/packages/OpenStrata.MSBuild.PCF)
- [ALM Project](https://www.nuget.org/packages/OpenStrata.MSBuild.ALM)
- [Publisher Project](https://www.nuget.org/packages/OpenStrata.MSBuild.Publisher)
- [Publisher ALM Project](https://www.nuget.org/packages/OpenStrata.MSBuild.Publisher.ALM)

***

Advanced publishers have the option of creating their own templates using the `openstrata-publisherdotnet` template.   Using this template, Publishers leverage the OpenStrata Framework to produce Production-Ready-To-DDCI capabilities while continuing to use tools and development kits of their choosing.

This template includes the following projects:

- [Publisher Project](https://www.nuget.org/packages/OpenStrata.MSBuild.Publisher)
- [Publisher ALM Project](https://www.nuget.org/packages/OpenStrata.MSBuild.Publisher.ALM)
- [Publisher New Project](https://www.nuget.org/packages/OpenStrata.MSBuild.Publisher.New)

To create an `openstrata-publisherdotnet` solution, run the following command.

```
dotnet new openstrata-publisherdotnet --name [preferred-publisher-name]
```



***


**About the OpenStrata Initiative**

The OpenStrata Initiative is an open-source project with the explicit objective to facilitate a standardized framework for Publishers and Consumers within the Microsoft Power Platform ecosystem to **Distribute**, **Discover**, **Consume**, and **Integrate** (DDCI) production-ready Power Platform 
capabilities.

