using Microsoft.Xrm.Sdk;
using System;

namespace PluginTemplate._1
{
    /// <summary>
    /// Plugin development guide: https://docs.microsoft.com/powerapps/developer/common-data-service/plug-ins
    /// Best practices and guidance: https://docs.microsoft.com/powerapps/developer/common-data-service/best-practices/business-logic/
    /// </summary>
    public abstract class TablePluginBase : PluginBase
    {

        public abstract string PrimaryEntityName { get; }



        protected TablePluginBase(Type pluginClassName) : base(pluginClassName) { }

        //
        protected override void ExecuteDataversePlugin(ILocalPluginContext localPluginContext)
        {
            if (localPluginContext == null)
            {
                throw new ArgumentNullException(nameof(localPluginContext));
            }

            


            var context = localPluginContext.PluginExecutionContext;

            if (context.PrimaryEntityName.ToLower() != PrimaryEntityName.ToLower())
            {
                throw new InvalidPluginExecutionException($"Attempting plugin on primary entity {context.PrimaryEntityName}.  Plugin execution limited to {PrimaryEntityName}.");
            }


            ExecuteDataverseEntityPlugin(context.MessageName, context, localPluginContext.TracingService, localPluginContext);

            // TODO: Implement your custom business logic

        }

        // Entry point for custom business logic execution
        protected abstract void ExecuteDataverseEntityPlugin(string messageName, IPluginExecutionContext context, ITracingService tracing, ILocalPluginContext localPluginContext);

    }
}
