using DataContractLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            if (Request != null)
            {
                Console.WriteLine("Request is null");
                AccountInfoService.AccountInfoServiceClient service = new AccountInfoService.AccountInfoServiceClient();
                var personalInfo = service.ViewPersonalInfo(Request.UniqueId);
                string address1 = personalInfo.Address1;
                string address2 = personalInfo.Address2;
                string city = personalInfo.City;
                int pinCode = personalInfo.PinCode;
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
