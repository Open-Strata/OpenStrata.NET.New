using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OstrataTemplate._1
{
    public static class Extensions
    {
        public static void ToTitleCase (this Entity entity, string attribute)
        {
            if (entity.TryGetAttributeValue<string>(attribute, out string attrValue))
            {
                entity.Attributes[attribute] = new CultureInfo("en-US", false).TextInfo.ToTitleCase(attrValue.ToLower());
            }
        }

        public static string AttributeValueOrAlternate(this Entity entity, string attribute, Entity altEntity, string defaultValue)
        {
            return entity.AttributeValueOrAlternate<string>(attribute, altEntity, defaultValue);
        }

        public static int? AttributeValueOrAlternate(this Entity entity, string attribute, Entity altEntity, int? defaultValue)
        {
            return entity.AttributeValueOrAlternate<int?>(attribute, altEntity, defaultValue);
        }

        public static T AttributeValueOrAlternate<T>(this Entity entity, string attribute, Entity altEntity, T defaultValue = default(T))
        {
            if (entity != null && entity.Contains(attribute)) return (T)entity[attribute];
            if (altEntity != null && altEntity.Contains(attribute)) return (T)altEntity[attribute];
            return defaultValue;
        }

        public static string AliasedValue(this Entity entity, string attribute)
        {
            return entity.AliasedValue<string>(attribute);
        }

        public static T AliasedValue<T> (this Entity entity, string attribute, T dafaultValue = default(T))
        {
            if (entity.Contains(attribute))
                return (T)((AliasedValue)entity[attribute]).Value;
            return dafaultValue;
        }
        public static void AddOrSetAttributeValue<T>(this Entity entity, string attribute, T value)
        {
            if (entity.Contains(attribute))
            {
                entity[attribute] = value;
            }
            else
            {
                entity.Attributes.Add(attribute, value);
            }
        }

        public static Entity RetrieveFetchXmlFirstOrNull(this IOrganizationService orgService, string fetchXml, ITracingService tracing)
        {

            tracing?.Trace($"RetrieveFetchXmlFirstOrNull");
            tracing?.Trace(fetchXml);

            EntityCollection result = orgService.RetrieveMultiple(new FetchExpression(fetchXml));

            tracing?.Trace($"FetchXml result entity count: {result.Entities.Count}");

            if (result.Entities.Count > 0)
            {
                return result.Entities[0];
            }
            return null;
        }

        public static T GetEnvironmentVariable<T>(this IOrganizationService orgservice, string envVariableName, T defaultValue, ITracingService tracing)
        {
            //T envVariableValue = defaultValue;

            string fetchXml = string.Format(@"<fetch version=""1.0"" output-format=""xml-platform"" mapping=""logical"" distinct=""false"">
            <entity name=""environmentvariablevalue"">
            <attribute name=""environmentvariablevalueid"" />
            <attribute name=""value"" />
            <link-entity name=""environmentvariabledefinition"" from=""environmentvariabledefinitionid"" to=""environmentvariabledefinitionid"" link-type=""inner"">
                <attribute name=""displayname"" />
                <filter type=""and"">
                <condition attribute=""schemaname"" operator=""eq"" value=""{0}"" />
                </filter>
            </link-entity>
            </entity>
           </fetch>"
            , envVariableName);

            tracing.Trace(fetchXml);

            EntityCollection result = orgservice.RetrieveMultiple(new FetchExpression(fetchXml));

            tracing.Trace($"FetchXml result entity count: {result.Entities.Count}");

            if (result != null && result.Entities.Count > 0)
            {

                var entity = result.Entities[0];

                //tracing.Trace($"Environment Variable Value Id: {entity.Id.ToString("D")}");

                //foreach (string key in entity.Attributes.Keys)
                //{
                //    tracing.Trace($"Environment Variable Attribute {key} type: {entity[key].GetType()}");
                //    tracing.Trace($"Environment Variable Attribute {key} Value: {entity[key]} ");
                //}

                //tracing.Trace($"Environment Variable returning value");

                return (T)result.Entities[0]["value"];
            }
            return defaultValue;
        }

        //public static EntityReference GetDocumentTemplateByName(this IOrganizationService orgService, EntityMetadata entityMetadata,  string name, int docType, ITracingService tracing)
        //{
        //    var fetchXml = String.Format(Resources.FetchDocumentTemplateXml, name, entityMetadata.ObjectTypeCode, docType);

        //    tracing.Trace($"Getting document template by name: {name}");

        //    var templateEntity = orgService.RetrieveFetchXmlFirstOrNull(fetchXml, tracing);

        //    tracing.Trace($"Getting document template by name: {name}");

        //    return templateEntity.ToEntityReference();

        //}

        public static EntityMetadata GetEntityMetadata (this Entity entity, IOrganizationService orgService, ITracingService tracing) 
        {
            return entity.ToEntityReference().GetEntityMetadata(orgService, tracing);
        }

        public static EntityMetadata GetEntityMetadata(this EntityReference entity, IOrganizationService orgService, ITracingService tracing)
        {

            RetrieveEntityRequest retrieveEntityRequest = new RetrieveEntityRequest
            {
                EntityFilters = EntityFilters.Entity,
                LogicalName = entity.LogicalName
            };

            tracing.Equals($"Attempting RetrieveEntityRequest: {entity.LogicalName}");

            RetrieveEntityResponse retrieveAccountEntityResponse = (RetrieveEntityResponse)orgService.Execute(retrieveEntityRequest);

            return retrieveAccountEntityResponse.EntityMetadata;

        }

        //public static EntityReference GetDocumentTemplateByName(this Entity entity, IOrganizationService orgService, string name, DocumentTemplateType docType, ITracingService tracing )
        //{
        //    return entity.ToEntityReference().GetDocumentTemplateByName(orgService,name,docType,tracing);
        //}



        //public static void IsNullOrEmpty <t> (this Entity entity, string attribute)
        //{
        //    if (entity.TryGetAttributeValue<t>(attribute, out t attrValue))
        //    {
        //         if (attrValue == null) return true;
        //         return false;
        //    }
        //    return true;
        //}        

        public static Entity NewEntity(this EntityReference entityReference)
        {
            return new Entity(entityReference.LogicalName, entityReference.Id);
        }

        public static Entity NewEntity(this Entity entity)
        {
            return new Entity(entity.LogicalName, entity.Id);
        }

        public static bool NullOrWrongType(this EntityReference entity, string logicalname)
        {
            return (entity == null || entity.LogicalName != logicalname);
        }

        public static bool NullOrWrongType(this Entity entity, string logicalname)
        {
            return (entity == null || entity.LogicalName != logicalname);
        }

    }

}
