using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Extensions;
using Microsoft.Xrm.Sdk.PluginTelemetry;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Runtime.CompilerServices;
using System.ServiceModel;

namespace OstrataTemplate._1
{
    /// <summary>
    /// Base class for all plug-in classes.
    /// Plugin development guide: https://docs.microsoft.com/powerapps/developer/common-data-service/plug-ins
    /// Best practices and guidance: https://docs.microsoft.com/powerapps/developer/common-data-service/best-practices/business-logic/
    /// </summary>
    public abstract partial class PluginBase : IPlugin
    {
        protected string PluginClassName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginBase"/> class.
        /// </summary>
        /// <param name="pluginClassName">The <see cref="Type"/> of the plugin class.</param>
        internal PluginBase(Type pluginClassName)
        {
            PluginClassName = pluginClassName.ToString();
        }

        /// <summary>
        /// Main entry point for he business logic that the plug-in is to execute.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <remarks>
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Execute")]
        public void Execute(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new InvalidPluginExecutionException(nameof(serviceProvider));
            }

            // Construct the local plug-in context.
            var localPluginContext = new LocalPluginContext(serviceProvider);

            localPluginContext.Trace($"Entered {PluginClassName}.Execute() " +
                $"Correlation Id: {localPluginContext.PluginExecutionContext.CorrelationId}, " +
                $"Initiating User: {localPluginContext.PluginExecutionContext.InitiatingUserId}");

            try
            {
                // Invoke the custom implementation
                ExecuteDataversePlugin(localPluginContext);

                // Now exit - if the derived plugin has incorrectly registered overlapping event registrations, guard against multiple executions.
                return;
            }
            catch (FaultException<OrganizationServiceFault> orgServiceFault)
            {
                localPluginContext.Trace($"Exception: {orgServiceFault.ToString()}");

                throw new InvalidPluginExecutionException($"OrganizationServiceFault: {orgServiceFault.Message}", orgServiceFault);
            }
            finally
            {
                localPluginContext.Trace($"Exiting {PluginClassName}.Execute()");
            }
        }

        /// <summary>
        /// Placeholder for a custom plug-in implementation.
        /// </summary>
        /// <param name="localPluginContext">Context for the current plug-in.</param>
        protected virtual void ExecuteDataversePlugin(ILocalPluginContext localPluginContext)
        {
            // Do nothing.
        }
    }

    /// <summary>
    /// This interface provides an abstraction on top of IServiceProvider for commonly used PowerPlatform Dataverse Plugin development constructs
    /// </summary>
    public interface ILocalPluginContext
    {

        /// <summary>
        /// The Guid Id of the PowerPlatform Dataverse organization Sytem User.
        /// </summary>
        Guid SystemUserId { get; }

        /// <summary>
        /// The PowerPlatform Dataverse organization service for the Current Executing user.
        /// </summary>
        IOrganizationService InitiatingUserService { get; }
        IOrganizationService userv { get; }

        /// <summary>
        /// The PowerPlatform Dataverse organization service for the Account that was registered to run this plugin, This could be the same user as InitiatingUserService.
        /// </summary>
        IOrganizationService PluginUserService { get; }
        /// <summary>
        /// Short form.  The PowerPlatform Dataverse organization service for the Account that was registered to run this plugin, This could be the same user as InitiatingUserService.
        /// </summary>        
        IOrganizationService pserv { get; }

        /// <summary>
        /// The PowerPlatform Dataverse organization service for the System.  This service generally has unlimited rights within the environment.
        /// </summary>
        IOrganizationService SystemUserService { get; }

        /// <summary>
        /// Short form.  The PowerPlatform Dataverse organization service for the System.  This service generally has unlimited rights within the environment.
        /// </summary>        
        IOrganizationService sserv { get; }

        /// <summary>
        /// IPluginExecutionContext contains information that describes the run-time environment in which the plug-in executes, information related to the execution pipeline, and entity business information.
        /// </summary>
        IPluginExecutionContext PluginExecutionContext { get; }
        /// <summary>
        /// Short form.  IPluginExecutionContext contains information that describes the run-time environment in which the plug-in executes, information related to the execution pipeline, and entity business information.
        /// </summary>        
        IPluginExecutionContext c { get; }

        /// <summary>
        /// Synchronous registered plug-ins can post the execution context to the Microsoft Azure Service Bus. <br/>
        /// It is through this notification service that synchronous plug-ins can send brokered messages to the Microsoft Azure Service Bus.
        /// </summary>
        IServiceEndpointNotificationService NotificationService { get; }

        /// <summary>
        /// Short form.  Synchronous registered plug-ins can post the execution context to the Microsoft Azure Service Bus. <br/>
        /// It is through this notification service that synchronous plug-ins can send brokered messages to the Microsoft Azure Service Bus.
        /// </summary>        
        IServiceEndpointNotificationService ns { get; }

        /// <summary>
        /// Provides logging run-time trace information for plug-ins.
        /// </summary>
        ITracingService TracingService { get; }
        /// <summary>
        /// Short form.Provides logging run-time trace information for plug-ins.
        /// </summary>
        ITracingService t { get; }

        /// <summary>
        /// General Service Provide for things not accounted for in the base class.
        /// </summary>
        IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Short form. General Service Provide for things not accounted for in the base class.
        /// </summary>        
        IServiceProvider sp { get; }

        /// <summary>
        /// OrganizationService Factory for creating connection for other then current user and system.
        /// </summary>
        IOrganizationServiceFactory OrgSvcFactory { get; }
        IOrganizationServiceFactory osv { get; }

        /// <summary>
        /// Access Environment Variables
        /// </summary>
        IEnvironmentVariableService EnvironmentVariableService { get; }
        IEnvironmentVariableService evs { get; }

        /// <summary>
        /// ILogger for this plugin.
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// Writes a trace message to the trace log.
        /// </summary>
        /// <param name="message">Message name to trace.</param>
        void Trace(string message, [CallerMemberName] string method = null);
    }

    /// <summary>
    /// Plug-in context object.
    /// </summary>
    public class LocalPluginContext : ILocalPluginContext
    {


        private static Guid? _systemUserId;
        /// <summary>
        /// The Guid Id of the PowerPlatform Dataverse organization Sytem User.
        /// </summary>        
        public Guid SystemUserId
        {
            get
            {
                if (_systemUserId == null)
                {
                    var org = PluginUserService.Retrieve("organization", PluginExecutionContext.OrganizationId, new ColumnSet(new string[] { "systemuserid" }));
                    _systemUserId = (Guid)org["systemuserid"];
                }
                return _systemUserId.Value;
            }
        }


        private IOrganizationService _initiatingUserService = null;
        /// <summary>
        /// The PowerPlatform Dataverse organization service for the Current Executing user.
        /// </summary>
        public IOrganizationService InitiatingUserService
        {
            get
            {
                if (_initiatingUserService == null)
                {
                    //User who's action called the plugin.
                    _initiatingUserService = ServiceProvider.GetOrganizationService(PluginExecutionContext.InitiatingUserId);
                }
                return _initiatingUserService;
            }
        }


        private IOrganizationService _pluginUserService = null;
        /// <summary>
        /// The PowerPlatform Dataverse organization service for the Account that was registered to run this plugin, This could be the same user as InitiatingUserService.
        /// </summary>
        public IOrganizationService PluginUserService
        {
            get
            {
                if (_pluginUserService == null)
                {
                    // User that the plugin is registered to run as, Could be same as current user.
                    _pluginUserService = ServiceProvider.GetOrganizationService(PluginExecutionContext.UserId);
                }
                return _pluginUserService;
            }
        }

        private IOrganizationService _systemUserService = null;
        /// <summary>
        /// The PowerPlatform Dataverse organization service for the System.  This service generallyhas unlimited rights within the environment.
        /// </summary>
        public IOrganizationService SystemUserService
        {
            get
            {
                if (_systemUserService == null)
                {
                    // System User
                    _systemUserService = OrgSvcFactory.CreateOrganizationService(SystemUserId);
                }
                return _systemUserService;
            }
        }

        private IPluginExecutionContext _pluginExecutionContext = null;
        /// <summary>
        /// IPluginExecutionContext contains information that describes the run-time environment in which the plug-in executes, information related to the execution pipeline, and entity business information.
        /// </summary>
        public IPluginExecutionContext PluginExecutionContext
        {
            get
            {
                if (_pluginExecutionContext == null)
                {
                    _pluginExecutionContext = ServiceProvider.Get<IPluginExecutionContext>();
                }
                return _pluginExecutionContext;
            }
        }


        private IServiceEndpointNotificationService _notificationService = null;
        /// <summary>
        /// Synchronous registered plug-ins can post the execution context to the Microsoft Azure Service Bus. <br/>
        /// It is through this notification service that synchronous plug-ins can send brokered messages to the Microsoft Azure Service Bus.
        /// </summary>
        public IServiceEndpointNotificationService NotificationService
        {
            get
            {
                if (_notificationService == null)
                {
                    _notificationService = ServiceProvider.Get<IServiceEndpointNotificationService>();
                }
                return _notificationService;
            }
        }

        private ITracingService _tracingService = null;
        /// <summary>
        /// Provides logging run-time trace information for plug-ins.
        /// </summary>
        public ITracingService TracingService
        {
            get
            {
                if (_tracingService == null)
                {
                    _tracingService = new LocalTracingService(ServiceProvider);
                }
                return _tracingService;
            }
        }

        /// <summary>
        /// General Service Provider for things not accounted for in the base class.
        /// </summary>
        public IServiceProvider ServiceProvider { get; }


        private IOrganizationServiceFactory _organizationserviceFactory = null;
        /// <summary>
        /// OrganizationService Factory for creating connection for other then current user and system.
        /// </summary>
        public IOrganizationServiceFactory OrgSvcFactory
        {
            get
            {
                if (_organizationserviceFactory == null)
                {
                    _organizationserviceFactory = ServiceProvider.Get<IOrganizationServiceFactory>();
                }
                return _organizationserviceFactory;
            }
        }



        private IEnvironmentVariableService _environmentVariableService = null;
        /// <summary>
        /// Access Environment Variables
        /// </summary>
        public IEnvironmentVariableService EnvironmentVariableService
        {
            get
            {
                if (_environmentVariableService == null)
                {
                    _environmentVariableService = ServiceProvider.Get<IEnvironmentVariableService>();
                }
                return _environmentVariableService;
            }
        }


        private ILogger _logger = null;
        /// <summary>
        /// ILogger for this plugin.
        /// </summary>
        public ILogger Logger
        {
            get
            {
                if (_logger == null)
                {
                    _logger = ServiceProvider.Get<ILogger>();
                }
                return _logger;
            }
        }

        IOrganizationService ILocalPluginContext.userv => this.InitiatingUserService;

        IOrganizationService ILocalPluginContext.pserv => this.PluginUserService;

        IOrganizationService ILocalPluginContext.sserv => this.SystemUserService;

        IPluginExecutionContext ILocalPluginContext.c => this.PluginExecutionContext;

        IServiceEndpointNotificationService ILocalPluginContext.ns => this.NotificationService;

        ITracingService ILocalPluginContext.t => this.TracingService;

        IServiceProvider ILocalPluginContext.sp => this.ServiceProvider;

        IOrganizationServiceFactory ILocalPluginContext.osv => this.OrgSvcFactory;

        IEnvironmentVariableService ILocalPluginContext.evs => this.EnvironmentVariableService;

        /// <summary>
        /// Helper object that stores the services available in this plug-in.
        /// </summary>
        /// <param name="serviceProvider"></param>
        public LocalPluginContext(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new InvalidPluginExecutionException(nameof(serviceProvider));
            }

            ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Writes a trace message to the trace log.
        /// </summary>
        /// <param name="message">Message name to trace.</param>
        public void Trace(string message, [CallerMemberName] string method = null)
        {
            if (string.IsNullOrWhiteSpace(message) || TracingService == null)
            {
                return;
            }

            if (method != null)
                TracingService.Trace($"[{method}] - {message}");
            else
                TracingService.Trace($"{message}");
        }
    }

    /// <summary>
    /// Specialized ITracingService implementation that prefixes all traced messages with a time delta for Plugin performance diagnostics
    /// </summary>
    public class LocalTracingService : ITracingService
    {
        private readonly ITracingService _tracingService;

        private DateTime _previousTraceTime;

        public LocalTracingService(IServiceProvider serviceProvider)
        {
            DateTime utcNow = DateTime.UtcNow;

            var context = (IExecutionContext)serviceProvider.GetService(typeof(IExecutionContext));

            DateTime initialTimestamp = context.OperationCreatedOn;

            if (initialTimestamp > utcNow)
            {
                initialTimestamp = utcNow;
            }

            _tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            _previousTraceTime = initialTimestamp;
        }

        public void Trace(string message, params object[] args)
        {
            var utcNow = DateTime.UtcNow;

            // The duration since the last trace.
            var deltaMilliseconds = utcNow.Subtract(_previousTraceTime).TotalMilliseconds;

            _tracingService.Trace($"[+{deltaMilliseconds:N0}ms] - {string.Format(message, args)}");

            _previousTraceTime = utcNow;
        }
    }
}
