using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;

using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Utilities;
using MySequentialWorkflow.ServiceReference1;

namespace MySequentialWorkflow
{
    public partial class Workflow2 : SequentialWorkflowActivity
    {
        private DateTime start, finish;
        CommonUtilities commonUtilities = new CommonUtilities();
        Service1Client service;
        //public Workflow2()
        //{
        //    //service = new MySequentialWorkflow.ServiceReference1.Service1Client();
        //}

        private Service1Client getService()
        {
            if (service == null)
            {
                service = new Service1Client();
            }
            return service;
        }

        #region ifElseActivity1 Block
        private void ifElseActivity11_ExecuteCode(object sender, ConditionalEventArgs e)
        {
            service = getService();
            start = DateTime.Now;
            string message = string.Empty;
            bool result = false;
            Console.WriteLine("Start Time");
            Console.WriteLine(start);
            Console.WriteLine("Please enter the account that you want to verify !!!");
            string accountNumber = Console.ReadLine();
            result = commonUtilities.checkUserAccountIsActive(accountNumber);
            if (result)
            {
                message = "User Accout is Active !!!";

                MySequentialWorkflow.ServiceReference1.AccountDetail account = service.GetAccountDetails(accountNumber);
                Console.WriteLine("Account Number : {0}", account.AccNumber);
            }
            else
            {
                message = "User Accout is not Active !!!";
            }
            Console.WriteLine(message);
            e.Result = result;

        }

        private void codeActivity11_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine("Account Owner is allowed for transactions !!!");
        }

        private void ifElseActivity12_ExecuteCode(object sender, ConditionalEventArgs e)
        {
            service = getService();
            string message = string.Empty;
            bool result = false;
            Console.WriteLine("Please enter the account that you want to re verify !!!");
            string accountNumber = Console.ReadLine();
            result = commonUtilities.checkUserIsInActiveScheme(accountNumber);
            if (result)
            {
                message = "User Accout is Active !!!";

                MySequentialWorkflow.ServiceReference1.AccountDetail account = service.GetAccountDetails(accountNumber);
                Console.WriteLine("Account Number : {0}", account.AccNumber);

            }
            else
            {
                message = "User Accout is not Active !!!";
            }
            Console.WriteLine(message);
            e.Result = result;
        }

        private void codeActivity12_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine("Account Owner is allowed for transactions !!!");
        }

        private void codeActivity1_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine("Activity flow is completed !!!");
            Console.WriteLine("");
            Console.WriteLine("****************");
            Console.WriteLine("");
        }

        #endregion ifElseActivity1 Block

        #region ifElseActivity2 Block
        private void ifElseActivity21_ExecuteCode(object sender, ConditionalEventArgs e)
        {
            service = getService();
            string message = string.Empty;
            bool result = false;
            Console.WriteLine("Please enter the account that you want to verify Scheme for the Account !!!");
            string accountNumber = Console.ReadLine();
            result = commonUtilities.checkUserAccountIsActive(accountNumber);
            if (result)
            {
                message = "User is in Active Scheme !!!";

                MySequentialWorkflow.ServiceReference1.SchemeDetail scheme = service.GetSchemePreference(accountNumber);
                Console.WriteLine("Scheme Name : {0}", scheme.Name);
            }
            else
            {
                message = "User is not in Active Scheme !!!";
            }
            Console.WriteLine(message);
            e.Result = result;

        }

        private void codeActivity21_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine("User is eligible to avail scheme benifits !!!");
        }

        private void ifElseActivity22_ExecuteCode(object sender, ConditionalEventArgs e)
        {
            service = getService();
            string message = string.Empty;
            bool result = false;
            Console.WriteLine("Please enter the account that you want to re verify !!!");
            string accountNumber = Console.ReadLine();
            result = commonUtilities.checkUserIsInActiveScheme(accountNumber);
            if (result)
            {
                message = "User is in Active Scheme !!!";

                MySequentialWorkflow.ServiceReference1.SchemeDetail scheme = service.GetSchemePreference(accountNumber);
                Console.WriteLine("Scheme Name : {0}", scheme.Name);
            }
            else
            {
                message = "User is still not in Active Scheme !!!";
            }
            Console.WriteLine(message);
            e.Result = result;
        }

        private void codeActivity22_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine("User is eligible to avail scheme benifits !!!");
        }

        private void codeActivity2_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine("Scheme Activity flow is completed !!!");
            Console.WriteLine("");
            Console.WriteLine("****************");
            Console.WriteLine("");
        }
        #endregion ifElseActivity2 Block

        #region ifElseActivity3 Block
        private void ifElseActivity31_ExecuteCode(object sender, ConditionalEventArgs e)
        {
            service = getService();
            string message = string.Empty;
            bool result = false;
            Console.WriteLine("Please enter the fundmanger details that you want to verify !!!");
            string fundMangerId = Console.ReadLine();
            int ifundMangerId;
            int.TryParse(fundMangerId, out ifundMangerId);
            result = commonUtilities.checkUserIsFundManger(ifundMangerId);
            if (result)
            {
                message = "User is Fund Manger !!!";

                MySequentialWorkflow.ServiceReference1.FundManagerDetail fundManger = service.GetFundManager(ifundMangerId);
                Console.WriteLine("Fund Manger Details : {0}", fundManger.Description);
            }
            else
            {
                message = "User is not Fund Manger !!!";
            }
            Console.WriteLine(message);
            e.Result = result;

        }

        private void codeActivity31_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine("Fund Manger is allowed to take care of funds !!!");
        }

        private void ifElseActivity32_ExecuteCode(object sender, ConditionalEventArgs e)
        {
            service = getService();
            string message = string.Empty;
            bool result = false;
            Console.WriteLine("Please enter the fundmanger details that you want to verify !!!");
            string fundMangerId = Console.ReadLine();
            int ifundMangerId;
            int.TryParse(fundMangerId, out ifundMangerId);
            result = commonUtilities.checkUserIsFundManger(ifundMangerId);
            if (result)
            {
                message = "User is Fund Manger !!!";

                MySequentialWorkflow.ServiceReference1.FundManagerDetail fundManger = service.GetFundManager(ifundMangerId);
                Console.WriteLine("Fund Manger Details : {0}", fundManger.Description);
            }
            else
            {
                message = "User is not Fund Manger !!!";
            }
            Console.WriteLine(message);
            e.Result = result;
        }

        private void codeActivity32_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine("Fund Manger is allowed to take care of funds !!!");
        }

        private void codeActivity3_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine("Fund Manger Activity flow is completed !!!");
            Console.WriteLine("");
            Console.WriteLine("****************");
            Console.WriteLine("");
        }
        #endregion ifElseActivity3 Block

        #region ifElseActivity4 Block
        private void ifElseActivity41_ExecuteCode(object sender, ConditionalEventArgs e)
        {
            service = getService();
            string message = string.Empty;
            bool result = false;
            Console.WriteLine("Please enter the accounts (sender & reciever) that you want to verify !!!");
            Console.WriteLine("Please enter the Sender Account that you want to verify !!!");
            string accountNumberSender = Console.ReadLine();
            Console.WriteLine("Please enter the Reciever Account that you want to verify !!!");
            string accountNumberReceiver = Console.ReadLine();
            result = commonUtilities.checkUserIsAllowedTransfer(accountNumberSender, accountNumberReceiver);
            if (result)
            {
                message = "Sender Account and Reciever Accounts are Active !!!";

                MySequentialWorkflow.ServiceReference1.AccountDetail accountSender = service.GetAccountDetails(accountNumberSender);
                Console.WriteLine("Sender Account Number : {0}", accountSender.AccNumber);

                MySequentialWorkflow.ServiceReference1.AccountDetail accountReciever = service.GetAccountDetails(accountNumberReceiver);
                Console.WriteLine("Reciever Account Number : {0}", accountReciever.AccNumber);
            }
            else
            {
                message = "Sender Account and Reciever Accounts are not Active !!!";
            }
            Console.WriteLine(message);
            e.Result = result;

        }

        private void codeActivity41_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine("Now funds transfer is allowed between sender and reciever !!!");
        }

        private void ifElseActivity42_ExecuteCode(object sender, ConditionalEventArgs e)
        {
            service = getService();
            string message = string.Empty;
            bool result = false;
            Console.WriteLine("Please enter the accounts (sender & reciever) that you want to re verify !!!");
            Console.WriteLine("Please enter the Sender Account that you want to re verify !!!");
            string accountNumberSender = Console.ReadLine();
            Console.WriteLine("Please enter the Reciever Account that you want to re verify !!!");
            string accountNumberReceiver = Console.ReadLine();
            result = commonUtilities.checkUserIsAllowedTransfer(accountNumberSender, accountNumberReceiver);
            if (result)
            {
                message = "Sender Account and Reciever Accounts are Active !!!";

                MySequentialWorkflow.ServiceReference1.AccountDetail accountSender = service.GetAccountDetails(accountNumberSender);
                Console.WriteLine("Sender Account Number : {0}", accountSender.AccNumber);

                MySequentialWorkflow.ServiceReference1.AccountDetail accountReciever = service.GetAccountDetails(accountNumberReceiver);
                Console.WriteLine("Reciever Account Number : {0}", accountReciever.AccNumber);
            }
            else
            {
                message = "Sender Account and Reciever Accounts are not Active !!!";
            }
            Console.WriteLine(message);
            e.Result = result;
        }

        private void codeActivity42_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine("Now funds transfer is allowed between sender and reciever !!!");
        }

        private void codeActivity4_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine("Monely Transfer Activity flow is completed between Sender and Reciever !!!");
            Console.WriteLine("");
            Console.WriteLine("****************");
            Console.WriteLine("");

            finish = DateTime.Now;
            Console.WriteLine("End Time");
            Console.WriteLine(finish);
        }
        #endregion ifElseActivity4 Block
        

    }
}
