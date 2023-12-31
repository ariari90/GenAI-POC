using System;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;


namespace ServiceInterceptor
{

    /// <summary>
    /// The custom service instance provider
    /// </summary>
    public class InjectionInstanceProvider : IInstanceProvider
    {
        private Type serviceContractType;
        private static readonly IUnityContainer container;
        private Object _sync = new Object();

        /// <summary>
        /// Static constructor, required to add the Policy Injection into the Unity container.
        /// </summary>
        static InjectionInstanceProvider()
        {
            // Create the container
            container = new UnityContainer().AddNewExtension<Interception>();

            // Read PIAB config and apply it to the container
            IConfigurationSource configSource = ConfigurationSourceFactory.Create();
            PolicyInjectionSettings settings =
                (PolicyInjectionSettings)configSource.GetSection(PolicyInjectionSettings.SectionName);
            if (settings != null)
            {
                settings.ConfigureContainer(container, configSource);
            }
        }

        /// <summary>
        /// Default constructor of the instance provider.
        /// </summary>
        /// <param name="type">Type of WCF service to be intercepted</param>
        public InjectionInstanceProvider(Type type)
        {
            if (type != null && !type.IsInterface)
            {
                throw new ArgumentException("Specified Type must be an interface");
            }

            this.serviceContractType = type;

        }

        #region IInstanceProvider Members

        /// <summary>
        /// Returns a service object given the specified System.ServiceModel.InstanceContext object.
        /// </summary>
        /// <param name="instanceContext">The current System.ServiceModel.InstanceContext object</param>
        /// <returns>A user-defined service object</returns>
        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        /// <summary>
        /// Returns a service object given the specified System.ServiceModel.InstanceContext object.
        /// </summary>
        /// <param name="instanceContext">The current System.ServiceModel.InstanceContext object</param>
        /// <param name="message">The message that triggered the creation of a service object</param>
        /// <returns>A user-defined service object</returns>
        public object GetInstance(System.ServiceModel.InstanceContext instanceContext, System.ServiceModel.Channels.Message message)
        {
            Type type = instanceContext.Host.Description.ServiceType;

            // Use the unity container to set the default interceptor 
            // Supports interface or MarshallByRef object.
            if (serviceContractType != null)
            {
                lock (_sync)
                {

                    container.Configure<Interception>().SetDefaultInterceptorFor(serviceContractType, new TransparentProxyInterceptor());
                    container.RegisterType(serviceContractType, type);
                    return container.Resolve(serviceContractType);
                }
            }
            else
            {
                if (!type.IsMarshalByRef)
                {
                    throw new ArgumentException("Type Must inherit MarshalByRefObject if no ServiceInterface is Specified");
                }
                lock (_sync)
                {
                    container.Configure<Interception>()
                        .SetDefaultInterceptorFor(type, new TransparentProxyInterceptor());
                    return container.Resolve(type);
                }
            }
        }


        /// <summary>
        /// Called when an System.ServiceModel.InstanceContext object recycles a service object.
        /// </summary>
        /// <param name="instanceContext">The service's instance context.</param>
        /// <param name="instance">The service object to be recycled.</param>
        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            IDisposable disposable = instance as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }

        }

        #endregion
    }

}
