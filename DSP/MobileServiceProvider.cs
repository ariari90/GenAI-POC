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
    public class MobileServiceProvider: ServiceProviderBase
    {
        public static DependencyProperty ResponseProperty = System.Workflow.ComponentModel.DependencyProperty.Register("Response", typeof(InfoServiceResponse), typeof(MobileServiceProvider));
        public static DependencyProperty RequestProperty = System.Workflow.ComponentModel.DependencyProperty.Register("Request", typeof(AggregatorRequest), typeof(MobileServiceProvider));

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataContractLibrary.InfoServiceResponse Response
        {
            get
            {
                return ((InfoServiceResponse)(base.GetValue(MobileServiceProvider.ResponseProperty)));
            }

            set
            {
                base.SetValue(MobileServiceProvider.ResponseProperty, value);
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataContractLibrary.AggregatorRequest Request
        {
            get
            {
                return ((AggregatorRequest
                    )(base.GetValue(MobileServiceProvider.RequestProperty)));
            }

            set
            {
                base.SetValue(MobileServiceProvider.RequestProperty, value);
            }
        }

        public string Mobile { get; set; }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Console.WriteLine("Executing  MobileServiceProvider");

            Request = GetValueOfWorkflowVariable(this.Parent, "Request") as AggregatorRequest;

            if (Request != null)
            {
                Console.WriteLine("Request is null");
                AccountInfoService.AccountInfoServiceClient service = new AccountInfoService.AccountInfoServiceClient();
                var personalInfo = service.ViewPersonalInfo(Request.UniqueId);
                string mobile = personalInfo.Mobile;
                SetValueOfWorkflowVariable(this, "Mobile", mobile);
                
            }

            return base.Execute(executionContext);
        }
    }
}
