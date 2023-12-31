using System;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace ServiceInterceptor
{
    /// <summary>
    /// The attribute class for custom WCF behaviour
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectionBehaviorAttribute : Attribute, IContractBehavior, IContractBehaviorAttribute
    {

        /// <summary>
        /// Default constructor
        /// </summary>
        public InjectionBehaviorAttribute()
        {
        }

        #region IContractBehaviorAttribute Members

        /// <summary>
        /// The target contract type
        /// </summary>
        public Type TargetContract
        {
            get
            {
                //return null so we apply to all contracts
                return null;
            }
        }

        #endregion

        #region IContractBehavior Members

        /// <summary>
        /// Configures any binding elements to support the contract behavior.
        /// </summary>
        /// <param name="contractDescription">The contract description to modify.</param>
        /// <param name="endpoint">The endpoint to modify.</param>
        /// <param name="bindingParameters"> The objects that binding elements require to support the behavior.</param>
        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            // No implementation required
            return;
        }

        /// <summary>
        /// Implements a modification or extension of the client across a contract.
        /// </summary>
        /// <param name="contractDescription">The contract description for which the extension is intended.</param>
        /// <param name="endpoint">The endpoint</param>
        /// <param name="clientRuntime">The client runtime.</param>
        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            // No implementation required
            return;
        }

        /// <summary>
        /// Implements a modification or extension of the client across a contract.
        /// </summary>
        /// <param name="contractDescription">The contract description to be modified.</param>
        /// <param name="endpoint">The endpoint that exposes the contract.</param>
        /// <param name="dispatchRuntime">The dispatch runtime that controls service execution.</param>
        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            // use the custom instance provider in lieu of the default provider
            Type contractType = contractDescription.ContractType;
            dispatchRuntime.InstanceProvider = new InjectionInstanceProvider(contractType);
        }

        /// <summary>
        ///  Implement to confirm that the contract and endpoint can support the contract behavior.
        /// </summary>
        /// <param name="contractDescription">The contract to validate.</param>
        /// <param name="endpoint"> The endpoint to validate.</param>
        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
            // No implementation required
            return;
        }

        #endregion

    }

}
