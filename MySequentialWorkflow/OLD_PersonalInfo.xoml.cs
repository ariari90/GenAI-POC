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

namespace MySequentialWorkflow
{
    public partial class OLD_PersonalInfo : SequenceActivity
    {
        int _x;

        public int SetX
        {
            set
            {
                _x = value;
            }
        }

        public int Result
        {
            get
            {
                return _x;
            }
        }

        public int Test { get; set; }

        public OLD_PersonalInfo(int x)
        {
            _x = x;
            Console.WriteLine("Hello from constructor");
        }

        protected override void InitializeProperties() // override here

        {

            base.InitializeProperties();

            //dataHelper = new DataHelper(); // look ma, put initialization here
            Console.WriteLine("Init test");
        }

        private void codeActivity_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine("Account Owner is allowed for transactions !!!");
            Console.WriteLine("Value: " + _x);

        }

        private void codeActivity_ExecuteCode2(object sender, EventArgs e)
        {
            Console.WriteLine("Lelelele");
            _x = 90;
        }

        private void codeActivity_Stop(object sender, EventArgs e)
        {
            Console.WriteLine("Terminating workflow...");
        }
    }
}
