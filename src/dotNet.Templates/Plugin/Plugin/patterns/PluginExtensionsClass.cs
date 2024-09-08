using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Metadata;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static PluginTemplate._1.PluginBase;

namespace PluginTemplate._1
{
    public static class PluginExtensionsClass
    {
        public static bool PrimaryEntityIs(this IPluginExecutionContext c, string logicalName)
        {
            return c.PrimaryEntityName == logicalName;
        }

        public static bool IsAsync(this IPluginExecutionContext c) => c.Mode == ExecutionMode.Asynchronous;
        public static bool IsSync(this IPluginExecutionContext c) => c.Mode == ExecutionMode.Synchronous;
        public static bool IsPreOperate(this IPluginExecutionContext c) => c.Stage == ExecutionStage.PreOperation;
        public static bool IsPostOperateAsync(this IPluginExecutionContext c) => c.IsAsync() && c.IsPostOperate();
        public static bool IsPostOperate(this IPluginExecutionContext c)=> c.Stage == ExecutionStage.PostOperation;
        public static bool IsPreValidation(this IPluginExecutionContext c)=> c.Stage == ExecutionStage.PreValidation;
        public static bool IsCreate(this IPluginExecutionContext c) => c.MessageName == Messages.Create;
        public static bool IsCreate(this ILocalPluginContext lpc) => lpc.c().IsCreate();
        public static bool IsCreatePreValidation(this IPluginExecutionContext c) => c.IsCreate() && c.IsPreValidation();
        public static bool IsUpdate(this IPluginExecutionContext c) => c.MessageName == Messages.Update;
        public static bool IsUpdate(this ILocalPluginContext lpc) => lpc.PluginExecutionContext.MessageName == Messages.Update;
        public static bool IsUpdatePreValidation(this IPluginExecutionContext c) => c.IsUpdate() && c.IsPreValidation();
        public static bool IsDelete(this IPluginExecutionContext c) => c.MessageName == Messages.Delete;
        public static bool IsCreateOrUpdate (this IPluginExecutionContext c)=> c.IsCreate() || c.IsUpdate();



        public static bool TryRetrieveField<T>(this Entity e, ILocalPluginContext lpc, string fieldLogicalName , out T value, T defaultValue = default)
        {
            return e.TryRetrieveField(lpc.InitiatingUserService, fieldLogicalName, out value, defaultValue);
        }
        public static bool TryRetrieveField<T>(this Entity e, IOrganizationService s, string fieldLogicalName, out T value, T defaultValue = default)
        {
            value = default;
            return s.TryRetrieve(e.ToEntityReference(),new string[] {fieldLogicalName}, out Entity result) &&
                   result.TryAttribute(fieldLogicalName, out value,  defaultValue);
        }

        public static bool TryAttributeOrRetrieve<T>(this Entity e, IOrganizationService s, string fieldLogicalName, out T value, T defaultValue = default)
        {
            value = default;
            return e.TryAttribute(fieldLogicalName, out value, defaultValue) ||
                (s.TryRetrieve(e.ToEntityReference(), new string[] { fieldLogicalName }, out Entity result) &&
                   result.TryAttribute(fieldLogicalName, out value, defaultValue));
        }

        public static bool TryRetrieve(this Entity e, ILocalPluginContext lpc, string[] fields, out Entity result)
        {
            return lpc.InitiatingUserService.TryRetrieve(e.ToEntityReference(), fields, out result);
        }
        public static bool TryRetrieve(this Entity e, IOrganizationService s, string[] fields, out Entity result)
        {
            return s.TryRetrieve(e.ToEntityReference(), fields, out result);
        }

        public static bool TryRetrieve(this EntityReference entityReference, ILocalPluginContext lpc, string[] fields, out Entity result)
        {
            return lpc.InitiatingUserService.TryRetrieve(entityReference, fields, out result);
        }
        public static bool TryRetrieve(this EntityReference entityReference, IOrganizationService s, string[] fields, out Entity result)
        {
            return s.TryRetrieve(entityReference, fields, out result);
        }

        public static bool TryRetrieve(this ILocalPluginContext lpc, EntityReference entityReference, string[] fields, out Entity result)
        {
            return lpc.InitiatingUserService.TryRetrieve(entityReference, fields, out result);  
        }
        public static bool TryRetrieve(this IOrganizationService s, EntityReference entityReference, string[] fields, out Entity result)
        {
            result = null;
            if (entityReference == null) return false;
            result = s.Retrieve(entityReference.LogicalName, entityReference.Id, new ColumnSet(fields));
            return (result != null);
         }

        public static bool TryLookupRetrieve(this ILocalPluginContext lpc, Entity sourceEntity, string lookupField, string[] fields, out Entity result)
        {
            return lpc.InitiatingUserService.TryLookupRetrieve(sourceEntity, lookupField, fields, out result);  
        }
        public static bool TryLookupRetrieve(this IOrganizationService s, Entity sourceEntity, string lookupField, string[] fields, out Entity result)
        {
            result = null;
            if (sourceEntity == null) return false;
            if (sourceEntity.TryAttribute(lookupField, out EntityReference er))
            {
                return s.TryRetrieve(er, fields, out result); 
            }
            return false;
        }


        public static Entity TargetEntity(this IPluginExecutionContext c)
        {
            return (Entity)c.InputParameters[_target];
        }
        public static EntityReference TargetEntityRef(this IPluginExecutionContext c)
        {
            return (EntityReference)c.InputParameters[_target];
        }

        // Shorthand Methods
        public static T t<T>(this IPluginExecutionContext c)
            where T : class, IExtensibleDataObject
        {
            return (T)c.InputParameters[_target];
        }
        public static T t<T>(this ILocalPluginContext lpc)
           where T : class, IExtensibleDataObject
        {
            return (T)lpc.c().InputParameters[_target];
        }
        public static IPluginExecutionContext c(this ILocalPluginContext lpc) => lpc.PluginExecutionContext;

        public static bool MessageIs(this IPluginExecutionContext c,string m) 
        {
            return c.MessageName == m;
        }

        // Friendly Name Methods
        //public static T Target<T> (this IPluginExecutionContext c)
        //    where T : class, IExtensibleDataObject
        //{
        //    return c.t<T>();
        //}

        //public static T Target<T>(this ILocalPluginContext lpc)
        //   where T : class, IExtensibleDataObject
        //{
        //    return lpc.c().t<T>();
        //}

        public static bool TryAttributeOrAlternative<T>(this Entity e, string logicalName, Entity altEntity, out T value, T defaultValue = default)
        {
            return e.TryAttribute(logicalName, out value, defaultValue) || 
                   altEntity.TryAttribute(logicalName, out value, defaultValue);
        }

        public static bool TryTargetAttributeOrPreImage<T>(this IPluginExecutionContext c, string logicalName, string preImageName, out T value, T defualtValue = default)
        {
            return c.TryTargetAttribute<T>(logicalName, out value, defualtValue) ||
                   c.TryPreEntityImageAttribute<T>(preImageName, logicalName, out value, defualtValue);
        }
        public static bool TryTargetAttributeOrFirstPreImage<T>(this IPluginExecutionContext c, string logicalName, out T value, T defualtValue = default)
        {
            return c.TryTargetAttribute<T>(logicalName, out value, defualtValue) ||
                   c.TryPreEntityFirstImageAttribute<T>(logicalName, out value, defualtValue);
        }
        public static bool TryTargetAttributeOrPostImage<T>(this IPluginExecutionContext c, string logicalName, string postImageName, out T value, T defualtValue = default)
        {
            return c.TryTargetAttribute<T>(logicalName, out value, defualtValue) ||
                   c.TryPostEntityImageAttribute<T>(postImageName, logicalName, out value, defualtValue);
        }

        public static bool TryTargetAttributeOrFirstPostImage<T>(this IPluginExecutionContext c, string logicalName, out T value, T defualtValue = default)
        {
            return c.TryTargetAttribute<T>(logicalName, out value, defualtValue) ||
                   c.TryPostEntityFirstImageAttribute<T>(logicalName, out value, defualtValue);
        }

        public static bool TryFormattedValue(this Entity e, string logicalName, out string formattedValue, string defaultValue = default)
        {
            formattedValue = defaultValue;
            if (e.Contains(logicalName) && e.FormattedValues.Contains(logicalName))
            {
                formattedValue = e.FormattedValues[logicalName];
                return true;
            }
            return false;
        }

        public static bool TryAttribute<T>(this Entity e, string logicalName, out T value, T defaultValue = default)
        {

            value = defaultValue;
            if (e == null) return false;
            if (e.Contains(logicalName))
            {
                if (e[logicalName] == null) return false;
                value = (T)e[logicalName];
                return true;
            }
            return false;
        }

        public static bool TryTargetAttribute<T>(this IPluginExecutionContext c, string logicalName, out T value, T defaultValue = default)
        {
            value = defaultValue;
            return c.TryTarget(out Entity entity) && entity.TryAttribute<T>(logicalName, out value, defaultValue);
        }

        public static bool TryFirstAttributeEntity<T>(this Entity e, out T value, out string logicalName, T defualtValue = default)
        {
            logicalName = e.Attributes.Keys.First();
            return e.TryAttribute<T>(logicalName, out value, defualtValue);
        }

        public static bool TryFirstAttributeTarget<T>(this IPluginExecutionContext c, out T value, out string logicalName, T defualtValue = default)
        {
            value = defualtValue;
            logicalName = null;

            if (c.TryTarget(out Entity e) )
            {
                return e.TryFirstAttributeEntity<T>(out value, out logicalName, defualtValue);
            }
            return false;
        }

        public static bool TryTargetAttribute<T>(this ILocalPluginContext lpc, string logicalName, out T value, T defaultValue = default)
        {
            return lpc.c().TryTargetAttribute<T>(logicalName, out value, defaultValue);
        }
 
        public static bool IsTrue(this Entity e, string logicalName)
        {
            e.TryAttribute<bool>(logicalName, out bool value, false);
            return value;
        }

        public static bool IsTrue(this IPluginExecutionContext c, string logicalName)
        {
            return c.TryTarget(out Entity entity) && 
                   entity.IsTrue(logicalName);
        }

        public static bool TryTarget<T>(this IPluginExecutionContext c, out T entity, T defaultValue = null)
            where T : class, IExtensibleDataObject
        {
            entity = defaultValue;

            if (c.InputParameters.Contains(_target) &&
                c.InputParameters[_target] != null &&
                c.InputParameters[_target] is T)
            {
                entity = (T)c.InputParameters[_target];
                return true;
            }
            return false;
        }

        public static bool TryTargetEntity(this IPluginExecutionContext c, out Entity entity, Entity defaultValue = null)
        {
            return c.TryTarget(out entity, defaultValue);
        }

        public static bool TryTargetEntityRef(this IPluginExecutionContext c, out EntityReference entity, EntityReference defaultValue = null)
        {
            return c.TryTarget(out entity, defaultValue);
        }

        public static bool TryPreEntityImage(this IPluginExecutionContext c,string imageName, out Entity entityImage, Entity defaultValue = null)
        {
            entityImage = defaultValue;

            if (c.PreEntityImages.Contains(imageName) &&
                c.PreEntityImages[imageName] != null)
            {
                entityImage = c.PreEntityImages[imageName];
                return true;
            }
            return false;
        }

        public static bool TryPreEntityFirstImageAttribute<T>(this IPluginExecutionContext c, string logicalName, out T value, T defaultValue = default)
        {
            value = defaultValue;
            return c.TryPreEntityFirstImage(out Entity preImage) &&
                   preImage.TryAttribute<T>(logicalName, out value);
        }

        public static bool TryPreEntityFirstImage(this IPluginExecutionContext c, out Entity entityImage, Entity defaultValue = null)
        {
            entityImage = defaultValue;
            return c.TryPreEntityFirstImageName(out string imageName) &&
                c.TryPreEntityImage(imageName,out entityImage, defaultValue);
        }
        public static bool TryPreEntityFirstImageName(this IPluginExecutionContext c, out string entityImageName)
        {
            entityImageName = c.PreEntityImages.Keys.FirstOrDefault();
            return c.PreEntityImages.Count > 0;
        }
        public static bool TryPreEntityImageAttribute<T>(this IPluginExecutionContext c, string imageName, string logicalName, out T value, T defaultValue = default)
        {
            value = defaultValue;
            return c.TryPreEntityImage(imageName, out Entity preImage) &&
                   preImage.TryAttribute<T>(logicalName, out value);
        }
        public static bool TryPostEntityImage (this IPluginExecutionContext c, string ImageName, out Entity entityImage, Entity defaultValue = null)
        {
            entityImage = defaultValue;

            if (c.PostEntityImages.Contains(ImageName) &&
                c.PostEntityImages[ImageName] != null)
            {
                entityImage = c.PostEntityImages[ImageName];
                return true;
            }
            return false;
        }
        public static bool TryPostEntityImageAttribute<T>(this IPluginExecutionContext c, string imageName, string logicalName, out T value, T defaultValue = default)
        {
            value = defaultValue;
            return c.TryPostEntityImage(imageName, out Entity postImage) &&
                   postImage.TryAttribute<T>(logicalName, out value);
        }
        public static bool TryPostEntityFirstImage(this IPluginExecutionContext c, out Entity entityImage, Entity defaultValue = null)
        {
            entityImage = defaultValue;
            return c.TryPostEntityFirstImageName(out string imageName) &&
                c.TryPostEntityImage(imageName, out entityImage, defaultValue);
        }
        public static bool TryPostEntityFirstImageName(this IPluginExecutionContext c, out string entityImageName)
        {
            entityImageName = c.PostEntityImages.Keys.FirstOrDefault();
            return c.PostEntityImages.Count > 0;
        }
        public static bool TryPostEntityFirstImageAttribute<T>(this IPluginExecutionContext c, string logicalName, out T value, T defaultValue = default)
        {
            value = defaultValue;
            return c.TryPostEntityFirstImage(out Entity postImage) &&
                   postImage.TryAttribute<T>(logicalName, out value, defaultValue);
        }

        private const string _target = "Target";
        private static class ExecutionStage
        {
            public static readonly int PreValidation = 10;
            public static readonly int PreOperation = 20;
            public static readonly int PostOperation = 40;
        }

        private static class ExecutionMode
        {
            public static readonly int Synchronous = 0;
            public static readonly int Asynchronous = 1;
        }
        public static class Messages
        {
            public static readonly string Update = "Update";
            public static readonly string Create = "Create";
            public static readonly string Delete = "Delete";
            public static readonly string Retrieve = "Retrieve";
        }

        public static Entity Retrieve(this IOrganizationService s, EntityReference entityReference, string[] fields)
        {
            s.TryRetrieve(entityReference, fields, out Entity result);
            return result;
        }

        public static Entity Set(this Entity e, string field, object value)
        {
            if (e.Contains(field)) { e[field] = value; }
            else { e.Attributes.Add(field, value); }
            return e;
        }

        //public static Entity RetrieveField<T>(this Entity e, IOrganizationService service, string field, out T value, T defaultValue = default)
        //{
        //    return service.Retrieve

        //    //return e.New()
        //    //       .Set(field, value)
        //    //       .Update(service);
        //}

        public static Entity Update(this Entity e, IOrganizationService service)
        {
            service.Update(e);
            return e;
        }
        public static Entity UpdateField (this Entity e, IOrganizationService service, string field, object value)
        {
            return e.New()
                   .Set(field, value)
                   .Update(service);
        }

        public static Entity UpdateField(this Entity e, ILocalPluginContext localPluginContext, string field, object value)
        {
            return e.UpdateField(localPluginContext.InitiatingUserService, field, value);
        }

        public static Entity ResetBitField(this Entity e, IOrganizationService service, string field)
        {
            return e.UpdateField(service, field, false);
        }
        public static Entity Create(this Entity e, IOrganizationService service)
        {
            var id = service.Create(e);
            e.Id = id;
            return e;
        }
        public static Entity New (this Entity e)
        {
            return new Entity(e.LogicalName, e.Id);
        }

        public static Entity New(this EntityReference r)
        {
            return new Entity(r.LogicalName, r.Id);
        }
    }


}
