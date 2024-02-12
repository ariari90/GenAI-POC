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
    public class BasicUserInfoServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataContractLibrary.AggregatorRequest Request
        {
            get; set;
        }

        public string FullName { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set;  }
        public string Nationality { get; set; }
        public DateTime DateOfBirth { get; set;  }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Console.WriteLine("Executing AddressServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;

            if (Request != null)
            {
                AccountInfoService.AccountInfoServiceClient service = new AccountInfoService.AccountInfoServiceClient();
                var personalInfo = service.ViewPersonalInfo(Request.UniqueId);
                
                SetDSFVariable(this, AggregatorConstants.FullName, personalInfo.FullName);
                SetDSFVariable(this, AggregatorConstants.FathersName, personalInfo.FathersName);
                SetDSFVariable(this, AggregatorConstants.MothersName, personalInfo.MothersName);
                SetDSFVariable(this, AggregatorConstants.Nationality, personalInfo.Nationality);
                SetDSFVariable(this, AggregatorConstants.DateOfBirth, personalInfo.DateOfBirth);
                SetDSFRequiredResponse(AggregatorConstants.InfoServiceResponse);
            }

            return base.Execute(executionContext);
        }
    }
}
