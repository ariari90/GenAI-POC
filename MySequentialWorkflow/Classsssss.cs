using DataContractLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;

namespace WorkflowConsoleApplication4
{
    [ActivityDesignerTheme(typeof(ClasssssDesignerTheme))]
    public class Classsssss : System.Workflow.ComponentModel.Activity
    {
        public static DependencyProperty ResponseProperty = System.Workflow.ComponentModel.DependencyProperty.Register("Response", typeof(InfoServiceResponse), typeof(Classsssss));

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataContractLibrary.InfoServiceResponse Response
        {
            get
            {
                return ((InfoServiceResponse)(base.GetValue(Classsssss.ResponseProperty)));
            }

            set
            {
                base.SetValue(Classsssss.ResponseProperty, value);
            }
        }

                    //public DataContractLibrary.InfoServiceResponse Response { get; set; }


        public ActivityBind Binding { get; set; }

        public int MyProperty { get; set; }
        public string UserName { get; set; }

        public Classsssss()
        {
            Console.WriteLine("Init Classssss");
        }
        public void Test()

        {
            Console.WriteLine("Init Classssss.Test");
        }

        private void InitializeComponent()
        {

            Console.WriteLine("Inside Initialize");
        }


        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            Console.WriteLine("Inside Classss constructor");
            InitializeComponent();

            

            if (Response == null)
            {
                Console.WriteLine("Response is null");
            }
            else
            {
                Console.WriteLine("Response is not null");
                if (Response.PersonalInfo != null)
                {
                    Console.WriteLine("Address is: " + Response.PersonalInfo.Address1);
                }
            }

            
            return ActivityExecutionStatus.Closed;
        }
    }

    public sealed class ClasssssDesignerTheme : ActivityDesignerTheme
    {
        public ClasssssDesignerTheme(WorkflowTheme theme)
            : base(theme)
        {
            this.ForeColor = Color.FromArgb(0xff, 0, 0, 0);
            this.BorderColor = Color.FromArgb(0xff, 0x73, 0x79, 0xa5);
            this.BorderStyle = DashStyle.Solid;
            this.BackColorStart = Color.FromArgb(209, 169, 255, 0xff);
            this.BackColorEnd = Color.FromArgb(168, 81, 255, 0xff);
            this.BackgroundStyle = LinearGradientMode.Horizontal;
        }
    }

    
    
}
