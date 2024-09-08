using Microsoft.Xrm.Sdk;
using System;
using System.Runtime.Remoting.Contexts;
using System.Web.UI.WebControls;

namespace PluginTemplate._1
{
    /// <summary>
    /// Plugin development guide: https://docs.microsoft.com/powerapps/developer/common-data-service/plug-ins
    /// Best practices and guidance: https://docs.microsoft.com/powerapps/developer/common-data-service/best-practices/business-logic/
    /// </summary>
    public abstract class StrongStagePluginBase : PluginBase
    {

        protected string UnsecureConfiguration { get; private set; }
        protected string SecureConfiguration { get; private set; }

        protected StrongStagePluginBase(Type pluginClassName, string unsecureConfiguration, string secureConfiguration) : base(pluginClassName) 
        {
            UnsecureConfiguration = unsecureConfiguration;
            SecureConfiguration = secureConfiguration;
        }

        protected virtual bool PreExecuteValidation(IPluginExecutionContext c, ITracingService t, ILocalPluginContext lpc) => true;

        //
        protected sealed override void ExecuteDataversePlugin(ILocalPluginContext lpc)
        {
            if (lpc == null)
            {
                throw new ArgumentNullException(nameof(lpc));
            }

            var tracing = lpc.TracingService;
            var context = lpc.PluginExecutionContext;

            if (!PreExecuteValidation(context,tracing,lpc))
            {
                tracing.Trace("Did pass pre execution validation.  Skipping step.");
                return;
            }

            if (context.IsPostOperateAsync())
            {
                tracing.Trace($"{this.GetType()}: Executing PostOperationAsync()");
                ExecutePostOperationAsync(context.MessageName, context, lpc.TracingService, lpc);
                tracing.Trace($"{this.GetType()}: Finished Executing PostOperationAsync()");
            }
            else if (context.IsPreOperate())
            {
                tracing.Trace($"{this.GetType()}: Executing PreOperation");
                ExecutePreOperation(context.MessageName, context, lpc.TracingService, lpc);
                tracing.Trace($"{this.GetType()}: Finished Executing PreOperation");
            }
            else if (context.IsPreValidation())
            {
                tracing.Trace($"{this.GetType()}: Executing PreValidation");
                ExecutePreValidation(context.MessageName, context, lpc.TracingService, lpc);
                tracing.Trace($"{this.GetType()}: Finished Executing PreValidation");
            }
            else if (context.IsPostOperate())
            {
                tracing.Trace($"{this.GetType()}: Executing PostOperation");
                ExecutePostOperation(context.MessageName, context, lpc.TracingService, lpc);
                tracing.Trace($"{this.GetType()}: Finished Executing PostOperation");
            }
            else
            {
                tracing.Trace($"{this.GetType()}: Executing Operation");
                ExecuteOperation(context.MessageName, context, lpc.TracingService, lpc);
                tracing.Trace($"{this.GetType()}: Finished Executing Operation");
            }
            // TODO: Implement your custom business logic

        }
        protected virtual void ExecutePreValidation(string m, IPluginExecutionContext c, ITracingService t, ILocalPluginContext lpc) { }
        protected virtual void ExecutePostOperationAsync(string m, IPluginExecutionContext c, ITracingService t, ILocalPluginContext lpc) { }
        protected virtual void ExecutePostOperation(string m, IPluginExecutionContext c, ITracingService t, ILocalPluginContext lpc) { }
        protected virtual void ExecutePreOperation(string m, IPluginExecutionContext c, ITracingService t, ILocalPluginContext lpc) { }
        protected virtual void ExecuteOperation(string m, IPluginExecutionContext c, ITracingService t, ILocalPluginContext lpc) { }

    }
}
