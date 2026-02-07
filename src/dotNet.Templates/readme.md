# OpenStrata.NET.Templates

[![NuGet](https://img.shields.io/nuget/v/OpenStrata.NET.Templates.svg)](https://www.nuget.org/packages/OpenStrata.NET.Templates/)
[![License: APSL-2.0](https://img.shields.io/badge/License-APSL%202.0-blue.svg)](https://opensource.org/licenses/APSL-2.0)

**OpenStrata.NET.Templates** provides comprehensive `dotnet new` templates for building enterprise-ready Microsoft Power Platform solutions. This template pack revolutionizes Power Platform development by introducing NuGet-style package management, enabling teams to create reusable "Strati" packages with automatic dependency resolution and streamlined deployment workflows.

## 🎯 What is OpenStrata?

OpenStrata transforms Power Platform development from manual, copy-paste solutions into a modern package-based ecosystem. Create **Strati packages** - NuGet packages containing complete Power Platform capabilities (Dataverse solutions, deployment logic, configuration data, plugins) that can reference other Strati packages, enabling true component reuse with automatic dependency sequencing.

**Before OpenStrata**: Manual deployments, copy-paste solutions, dependency coordination nightmares  
**With OpenStrata**: Package-based development, automated deployments, zero-effort dependency management

## 🚀 Quick Start

### Prerequisites
- .NET SDK 6.0 or later
- Power Platform CLI (pac)
- Microsoft Dataverse environment

### Installation

Install the templates using the .NET CLI:

```bash
dotnet new install OpenStrata.NET.Templates
```

### Create Your First Solution

```bash
# Create a new folder for your solution
mkdir ContosoSales
cd ContosoSales

# Generate complete OpenStrata solution
dotnet new os-dotnet -pn "Contoso Corporation" -pp "contoso"

# Build and create your first Strati package
dotnet build
```

This creates a complete solution with:
- **Solution** project for Dataverse components
- **Package** project for Package Deployer orchestration
- **Strati** project for NuGet package creation
- **Deployment** project for custom deployment logic
- **ConfigData** project for configuration and reference data

## 📦 Available Templates

### Solution Templates

- **os-dotnet** - Complete OpenStrata solution with all core projects

### Component Project Templates

Add these to existing OpenStrata solutions:

- **os-plugin** - Dynamics 365 plugin development project
- **os-pcf** - Power Apps Component Framework control project
- **os-powerpages** - Power Pages website project
- **os-doctemplates** - Document templates project

### Core Infrastructure Templates

- **os-solution** - Dataverse solution project (.cdsproj)
- **os-package** - Package Deployer orchestration project
- **os-strati** - Strati manifest and NuGet packaging project
- **os-deployment** - Custom deployment logic project
- **os-configdata** - Configuration and reference data project

### Configuration Templates

- **os-essentials** - Essential configuration files and folder structure
- **os-props** - OpenStrata properties file
- **os-strataversions** - Strati version management file

## 💡 Usage Examples

```bash
# Create solution with custom name
dotnet new os-dotnet -n MyProject -pn "Contoso" -pp "contoso"

# Add plugin project to existing solution
dotnet new os-plugin -n MyPlugin

# Add PCF control project
dotnet new os-pcf -n MyControl

# Add Power Pages project
dotnet new os-powerpages -n MySite
```

## 🎓 Integration with OpenStrata Ecosystem

These templates are part of the comprehensive OpenStrata Initiative:

- **OpenStrata.MSBuild** - Core packaging and Strati creation engine
- **OpenStrata.Sdk** - MSBuild SDK infrastructure for project types
- **OpenStrata.DevOps** - Power Platform CLI automation, Git workflows, GitHub Actions integration
- **OpenStrata.Nuget** - Enhanced NuGet packaging for Strati distribution

All components work together seamlessly to provide an end-to-end Power Platform ALM solution.

## 📚 Documentation

- [Template Documentation](https://github.com/Open-Strata/OpenStrata.NET.New/blob/main/docs/TEMPLATES.md) - Detailed template reference
- [How OpenStrata Works](https://github.com/Open-Strata/OpenStrata.MSBuild/blob/main/HOW-IT-WORKS.md) - Technical deep dive
- [Contributing Guide](https://github.com/Open-Strata/OpenStrata.NET.New/blob/main/CONTRIBUTING.md) - Join the initiative
- [OpenStrata Initiative](https://github.com/Open-Strata) - GitHub organization

## 🌟 About the OpenStrata Initiative

The OpenStrata Initiative is an open-source project revolutionizing Microsoft Power Platform development by introducing modern package management practices. Our mission is to enable organizations to **Distribute, Discover, Consume, and Integrate (DDCI)** production-ready Power Platform capabilities through:

- **Reusable Strati Packages** - Complete capabilities packaged as NuGet packages
- **Dependency Management** - Automatic resolution of package dependencies
- **Automated Deployment** - Zero-effort dependency sequencing
- **Enterprise Scale** - Built for large organizations with multiple teams

## 🤝 Contributing

We welcome contributions! Please see our [Contributing Guidelines](https://github.com/Open-Strata/OpenStrata.NET.New/blob/main/CONTRIBUTING.md) for details.

## 📄 License

This project is licensed under the Apache License 2.0 - see the [LICENSE](LICENSE.txt) file for details.

---

**Copyright © 74Bravo LLC and Contributors. All rights reserved.**

