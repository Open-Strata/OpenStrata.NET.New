using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginTemplate._1
{
    public class SetDateBitAttrInstruction : StrongStagePluginBase
    {
        protected string DateFieldLogicalName => this.UnsecureConfiguration;

        public SetDateBitAttrInstruction(string unsecureConfiguration, string secureConfiguration) 
            : base(typeof(SetDateBitAttrInstruction), unsecureConfiguration, secureConfiguration)
        {
        
        }

        protected override void ExecutePreValidation(string m, IPluginExecutionContext c, ITracingService t, ILocalPluginContext lpc)
        {

           if (!string.IsNullOrEmpty(DateFieldLogicalName) &&
                c.IsCreateOrUpdate() &&
                c.TryTarget(out Entity target) && 
                target.TryFirstAttributeEntity(out bool setBit, out string setBitlogicalName) &&
                setBit)
            {
                target.Set(DateFieldLogicalName, DateTime.Now);
                c.SharedVariables.AddOrUpdateIfNotNull(DateFieldLogicalName, setBitlogicalName);
            }
        }

        protected override void ExecutePostOperationAsync(string m, IPluginExecutionContext c, ITracingService t, ILocalPluginContext lpc)
        {

            t.Trace($"ExecutePostOperationAsync: {DateFieldLogicalName}");

            if (!string.IsNullOrEmpty(DateFieldLogicalName) &&
                 c.IsCreateOrUpdate() &&
                 c.TryTarget(out Entity target) &&
                 target.TryFirstAttributeEntity(out bool setBit, out string setBitlogicalName) &&
                 setBit)
            {
                target.UpdateField(lpc.InitiatingUserService, setBitlogicalName, false);
            }
        }
    }
}
