using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.EntityClient;
using System.Data.Common;
using Microsoft.Practices.Unity;
using BRIMS.EmailAlerts.BusinessLogic.Data;
using BRIMS.EmailAlerts.BusinessLogic.Configuration;
using BRIMS.EmailAlerts.BusinessLogic.Alerts.BankExposures;
using BRIMS.EmailAlerts.BusinessLogic.Alerts.BrokerExposureAlerts;
using BRIMS.EmailAlerts.BusinessLogic.Alerts.RBALimitAlerts;
using BRIMS.EmailAlerts.BusinessLogic.Alerts.TransactionEmailAlerts;
using BRIMS.EmailAlerts.BusinessLogic.Alerts.UmbrellaAlerts;
using BRIMS.EmailAlerts.BusinessLogic.Email;
using BRIMS.EmailAlerts.BusinessLogic.System;


namespace BRIMS.EmailAlerts.BusinessLogic.Infrastructure
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        #region Fields

        private readonly IUnityContainer _container;

        #endregion

        #region Ctor

        public UnityDependencyResolver()
            : this(new UnityContainer())
        {
        }
        
        public UnityDependencyResolver(IUnityContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            this._container = container;
            //configure container
            ConfigureContainer(this._container);
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Configure root container.Register types and life time managers for unity builder process
        /// </summary>
        /// <param name="container">Container to configure</param>
        protected virtual void ConfigureContainer(IUnityContainer container)
        {
            //Take into account that Types and Mappings registration could be also done using the UNITY XML configuration
            //But we prefer doing it here (C# code) because we'll catch errors at compiling time instead execution time, if any type has been written wrong.

            //Register repositories mappings
            //to be done

            //Register default cache manager            
            //container.RegisterType<ICacheManager, NopRequestCache>(new PerExecutionContextLifetimeManager());

            //Register managers(services) mappings
            container.RegisterType<IBankExposureService, BankExposureService>(new UnityPerExecutionContextLifetimeManager());
            container.RegisterType<IBrokerExposureService, BrokerExposureService>(new UnityPerExecutionContextLifetimeManager());
            container.RegisterType<IRBALimitService, RBAMailAlertService>(new UnityPerExecutionContextLifetimeManager());
            container.RegisterType<ITransactionEmailService, TransactionEmailAlertService>(new UnityPerExecutionContextLifetimeManager());
            container.RegisterType<IUmbrellaEmailService, UmbrellaEmailService>(new UnityPerExecutionContextLifetimeManager());
            container.RegisterType<IMailService, MailService>(new UnityPerExecutionContextLifetimeManager());
            container.RegisterType<ISystemService, SystemService>(new UnityPerExecutionContextLifetimeManager());

          
            

            //Object context

            //Connection string
            //if (InstallerHelper.ConnectionStringIsSet())
            //{
                var ecsbuilder = new EntityConnectionStringBuilder();
                ecsbuilder.Provider = "System.Data.SqlClient";
                ecsbuilder.ProviderConnectionString = Config.ConnectionString;
                ecsbuilder.Metadata = @"res://*/Data.BRIMSModel.csdl|res://*/Data.BRIMSModel.ssdl|res://*/Data.BRIMSModel.msl";
                string connectionString = ecsbuilder.ToString();
                InjectionConstructor connectionStringParam = new InjectionConstructor(connectionString);
                //Registering object context
                container.RegisterType<BRIMSObjectContext>(new UnityPerExecutionContextLifetimeManager(), connectionStringParam);
            //}
        }

        #endregion

        #region Methods

        /// <summary>
        /// Register instance
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="instance">Instance</param>
        public void Register<T>(T instance)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            _container.RegisterInstance(instance);
        }

        /// <summary>
        /// Inject
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="existing">Type</param>
        public void Inject<T>(T existing)
        {
            if (existing == null)
                throw new ArgumentNullException("existing");

            _container.BuildUp(existing);
        }

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="type">Type</param>
        /// <returns>Result</returns>
        public T Resolve<T>(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return (T)_container.Resolve(type);
        }

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="type">Type</param>
        /// <param name="name">Name</param>
        /// <returns>Result</returns>
        public T Resolve<T>(Type type, string name)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (name == null)
                throw new ArgumentNullException("name");

            return (T)_container.Resolve(type, name);
        }

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Result</returns>
        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="name">Name</param>
        /// <returns>Result</returns>
        public T Resolve<T>(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            return _container.Resolve<T>(name);
        }

        /// <summary>
        /// Resolve all
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Result</returns>
        public IEnumerable<T> ResolveAll<T>()
        {
            IEnumerable<T> namedInstances = _container.ResolveAll<T>();
            T unnamedInstance = default(T);

            try
            {
                unnamedInstance = _container.Resolve<T>();
            }
            catch (ResolutionFailedException)
            {
                //When default instance is missing
            }

            if (Equals(unnamedInstance, default(T)))
            {
                return namedInstances;
            }

            return new ReadOnlyCollection<T>(new List<T>(namedInstances) { unnamedInstance });
        }

        #endregion
    }
}
