//using Microsoft.Uii.Common.Entities;
using Microsoft.Xrm.Tooling.PackageDeployment.CrmPackageExtentionBase;
using OpenStrata.Deployment.Sdk;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstrataTemplate._1.Deployment
{
    [Export(typeof(IImportPackageStratiExtension))]
    public class ImportPackageStrati : PackageStratiBase, IImportPackageStratiExtension
    {

        protected override bool AfterPrimaryImport()
        {
            return base.AfterPrimaryImport();
        }

        protected override bool AppliesToThisSolution(string solutionName)
        {
            return base.AppliesToThisSolution(solutionName);
        }

        //protected override ApplicationRecord BeforeApplicationRecordImport(ApplicationRecord app)
        //{
        //    return base.BeforeApplicationRecordImport(app);
        //}

        protected override bool BeforeImportStage()
        {
            return base.BeforeImportStage();
        }

        protected override int OverrideConfigurationDataFileLanguage(int selectedLanguage, List<int> availableLanguages)
        {
            return base.OverrideConfigurationDataFileLanguage(selectedLanguage, availableLanguages);
        }

        protected override UserRequestedImportAction OverrideSolutionImportDecision(UserRequestedImportAction userRequestedImportAction, string solutionUniqueName, Version organizationVersion, Version packageSolutionVersion, Version inboundSolutionVersion, Version deployedSolutionVersion, ImportAction systemSelectedImportAction)
        {
            return base.OverrideSolutionImportDecision(userRequestedImportAction, solutionUniqueName, organizationVersion, packageSolutionVersion, inboundSolutionVersion, deployedSolutionVersion, systemSelectedImportAction);
        }

        protected override void PreSolutionImport(string solutionName, bool solutionOverwriteUnmanagedCustomizations, bool solutionPublishWorkflowsAndActivatePlugins, out bool overwriteUnmanagedCustomizations, out bool publishWorkflowsAndActivatePlugins)
        {
            base.PreSolutionImport(solutionName, solutionOverwriteUnmanagedCustomizations, solutionPublishWorkflowsAndActivatePlugins, out overwriteUnmanagedCustomizations, out publishWorkflowsAndActivatePlugins);
        }

        protected override void RunSolutionUpgradeMigrationStep(string solutionName, string oldVersion, string newVersion, Guid oldSolutionId, Guid newSolutionId)
        {
            base.RunSolutionUpgradeMigrationStep(solutionName, oldVersion, newVersion, oldSolutionId, newSolutionId);
        }
    }
}
