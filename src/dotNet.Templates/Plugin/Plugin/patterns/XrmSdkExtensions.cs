using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginTemplate._1
{
    public static class XrmSdkExtensions
    {

        public static void UpdateField(this QueryBase qry, IOrganizationService service, string field, object value, Guid skipId = default)
        {
            service.RetrieveMultiple(qry)?.UpdateField(service, field, value, skipId);
        }

        public static void UpdateField(this EntityCollection entities, IOrganizationService service, string field, object value, Guid skipId = default)
        {
            foreach(var entity in entities.Entities)
            {
                if (entity.Id != skipId)
                {
                    entity.UpdateField(service, field, value);
                }
            }
        }

    }
}
