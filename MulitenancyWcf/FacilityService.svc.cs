using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.Text;
using DataContractLibrary;
using DecisionLibrary;
using System.Activities;
using System.ServiceModel.Activation;
using System.Collections;
using System.Web;

namespace MulitenancyWcf
{
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Required)]
    //[Shareable
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class FacilityService : IFacilityService
    {
        public FacilityResponse CreateFacility(FacilityRequest facilityRequest)
        {
            FacilityResponse facilityResponse = new FacilityResponse();
            MetaDataFetch metadataDecision = new MetaDataFetch();
            int iTenantId = 0;

            //InstanceContext instanceContext = OperationContext.Current.InstanceContext;
            //AddressableInstanceContextInfo info = instanceContext.Extensions.Find<AddressableInstanceContextInfo>();
            
            //string temp = ((info != null) ? info.InstanceId : null);
            //Hashtable hashTenants = DataContractLibrary.TenantData.InMemory.TenantSessionMap;
            // int threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            //int iTenantId = Convert.ToInt16( hashTenants[threadId]);

            if (HttpContext.Current.Session["TenantSession"] != null)
                iTenantId = Convert.ToInt32(HttpContext.Current.Session["TenantSession"].ToString()); 

            metadataDecision.GetProcessName(iTenantId, "CreateFacility");

            if (!string.IsNullOrEmpty(metadataDecision.AssemblyInvoke))
            {
                //invoke Varied process
                VariedProcessLoader varied = new VariedProcessLoader();
                varied.AssemblyNameInvoke = metadataDecision.AssemblyInvoke;
                varied.NamespaceClassNameInvoke = metadataDecision.NamespaceClassNameInvoke;
                varied.InputRequest = facilityRequest;
                varied.ExecuteVariedProcessLoad();                
                facilityResponse  = (FacilityResponse)varied.OutputResult["workflowOutput"];
            }
            else
            {
                //invoke core process
                CoreWorkflows.CreateFacility createFacility = new CoreWorkflows.CreateFacility();
                createFacility.InitializeComponent();
                Dictionary<string, object> inputs = new Dictionary<string, object>() { { "facilityRequest", facilityRequest } }; 
                IDictionary<string, object> results = WorkflowInvoker.Invoke(createFacility,inputs);
                facilityResponse = (FacilityResponse)results["faciliytResponse"];
            }

                return facilityResponse;
        }

        public FacilityResponse UpdateFacility(FacilityRequest value)
        {
            FacilityResponse facilityResponse = new FacilityResponse();

            return facilityResponse;
        }
    }
}
