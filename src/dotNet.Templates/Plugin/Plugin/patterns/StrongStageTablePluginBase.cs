using Microsoft.Xrm.Sdk;
using System;
using System.Runtime.Remoting.Contexts;

namespace PluginTemplate._1
{
    /// <summary>
    /// Plugin development guide: https://docs.microsoft.com/powerapps/developer/common-data-service/plug-ins
    /// Best practices and guidance: https://docs.microsoft.com/powerapps/developer/common-data-service/best-practices/business-logic/
    /// </summary>
    public abstract class StrongStageTablePluginBase : StrongStagePluginBase
    {

        public abstract string PrimaryEntityName { get; }

        protected StrongStageTablePluginBase(Type pluginClassName, string unsecureConfiguration, string secureConfiguration) :
            base(pluginClassName, unsecureConfiguration, secureConfiguration)
        { }

        protected override bool PreExecuteValidation(IPluginExecutionContext c, ITracingService t, ILocalPluginContext lpc)
        {
            if (c.PrimaryEntityName.ToLower() != PrimaryEntityName.ToLower())
            {
                throw new InvalidPluginExecutionException($"Attempting plugin on primary entity {c.PrimaryEntityName}.  Plugin execution limited to {PrimaryEntityName}.");
            }
            return base.PreExecuteValidation(c, t, lpc);
        }
    }
}
