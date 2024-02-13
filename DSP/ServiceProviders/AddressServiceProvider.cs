using DataContractLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;

namespace DSP
{
    public class AddressServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataContractLibrary.AggregatorRequest Request
        {
            get; set;
        }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set;  }
        public int PinCode { get; set;  }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Console.WriteLine("Executing  AddressServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;
            string address1 = null;
            string address2 = null;
            string city = null;
            int pinCode = default(int);

            try
            {
                if (Request != null)
                {
                    Console.WriteLine("Request is null");
                    AccountInfoService.AccountInfoServiceClient service = new AccountInfoService.AccountInfoServiceClient();
                    var personalInfo = service.ViewPersonalInfo(Request.UniqueId);
                    address1 = personalInfo.Address1;
                    address2 = personalInfo.Address2;
                    city = personalInfo.City;
                    pinCode = personalInfo.PinCode;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected error occured: " + e.ToString());
                throw new Exception("Workflow error: " + e.ToString());
            }
            finally
            {
                SetDSFVariable(this, AggregatorConstants.Address1, address1);
                SetDSFVariable(this, AggregatorConstants.Address2, address2);
                SetDSFVariable(this, AggregatorConstants.City, city);
                SetDSFVariable(this, AggregatorConstants.PinCode, pinCode);
                SetDSFRequiredResponse(AggregatorConstants.InfoServiceResponse);
            }
            
            return base.Execute(executionContext);
        }
    }
}
