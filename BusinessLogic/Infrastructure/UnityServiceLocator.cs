using System;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BRIMS.EmailAlerts.BusinessLogic.Infrastructure
{
    /// <summary>
    /// Unity implemenation of the <see cref="ServiceLocatorImplBase" />
    /// </summary>
    public class UnityServiceLocator : ServiceLocatorImplBase
    {
        readonly IUnityContainer _unityContainer;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unityContainer">Unity container</param>
        public UnityServiceLocator(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return _unityContainer.Resolve(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return _unityContainer.ResolveAll(serviceType);
        }
    }
}
