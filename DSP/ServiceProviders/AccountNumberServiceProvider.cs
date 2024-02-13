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
    public class AccountNumberServiceProvider: ServiceProviderBase
    {

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataContractLibrary.AggregatorRequest Request
        {
            get; set;
        }

        public int AccountNumber { get; set; }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Console.WriteLine("Executing  AccountNumberServiceProvider");

            Request = GetDSFVariable(this.Parent, "Request") as AggregatorRequest;
            BankInfo bankInfo = null;

            try
            {
                if (Request != null)
                {
                    Console.WriteLine("Request is null");
                    AccountInfoService.AccountInfoServiceClient service = new AccountInfoService.AccountInfoServiceClient();
                    bankInfo = service.ViewBankInfo(Request.UniqueId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected error occured: " + e.ToString());
                throw new Exception("Workflow error: " + e.ToString());
            }
            finally
            {
                if (bankInfo != null)
                {
                    SetDSFVariable(this, AggregatorConstants.AccountNumber, bankInfo.AccountNumber);
                    SetDSFRequiredResponse(AggregatorConstants.InfoServiceResponse);
                }
                
            }
            return base.Execute(executionContext);
        }
    }
}
