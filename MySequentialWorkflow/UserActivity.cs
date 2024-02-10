using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Workflow.ComponentModel;
using DataContractLibrary;

namespace WorkflowConsoleApplication4
{

    public partial class UserActivity : System.Workflow.ComponentModel.Activity
    {
        public string _password;
        public string _email;
        string _username;
        string _provider;

        [Category("Wows")]
        [System.ComponentModel.ReadOnly(true)]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }



        public string MembershipProviderName
        {
            get { return _provider; }
            set { _provider = value; }
        }

        public ActivityBind Binding { get; set; }

        //public static DependencyProperty ResponseProperty =  System.Workflow.ComponentModel.DependencyProperty.Register("Response", typeof(InfoServiceResponse), typeof(UserActivity));

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataContractLibrary.InfoServiceResponse Response { get; set; }

        //public static DependencyProperty UserNameProperty = System.Workflow.ComponentModel.DependencyProperty.Register("UserName", typeof(string), typeof(CreateUserActivity));

        [Description("The new username for this user")]
        [Category("User")]
        [Browsable(true)]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Visible)]
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }



        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Console.WriteLine("Inside Execute method...");
            UserName = "TTT";

            Response = new InfoServiceResponse();
            Response.PersonalInfo = new PersonalInfo();
            Response.PersonalInfo.Address1 = "abcdefg";
            Binding = new ActivityBind();
            Binding.Name = this.Name;
            Binding.Path = "Binding";
            //Binding.SetRuntimeValue(this, Response);
            Binding.UserData.Add("Response", Response);
            return base.Execute(executionContext);
        }

        
    }
}
