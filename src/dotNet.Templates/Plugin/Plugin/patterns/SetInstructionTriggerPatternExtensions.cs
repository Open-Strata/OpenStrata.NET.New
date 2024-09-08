using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginTemplate._1
{
    public static class SetInstructionTriggerPatternExtensions
    {

        public static void ResetSetInstruction(this Entity entity, IOrganizationService service, string setAttributeLogicalName, bool resetValue = false)
        {
            if (entity != null && service != null ) 
            {
                var resetEntity = entity.NewEntity();
                resetEntity.Attributes.Add(setAttributeLogicalName, resetValue);
                service.Update(resetEntity);
            }
        }

    }
}
