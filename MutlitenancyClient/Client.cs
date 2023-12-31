using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel.Channels;
using System.Security.Cryptography;
using System.ServiceModel;

namespace MutlitenancyClient
{
 
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        private void btnInvoke_Click(object sender, EventArgs e)
        {
            txtResult.Text = string.Empty;

            if (Validate())
            {
                FacilityServiceClient.FacilityServiceClient client = new FacilityServiceClient.FacilityServiceClient();

                MessageHeader Client1InstanceContextHeader = MessageHeader.CreateHeader(
                   "Tenant",
                   "Tenant",
                   cmbTenant.SelectedItem);

                try
                {
                    using (new OperationContextScope(client.InnerChannel))
                    {
                        //Add the header as a header to the scope so it gets sent for each message.
                        OperationContext.Current.OutgoingMessageHeaders.Add(Client1InstanceContextHeader);
                        FacilityServiceClient.FacilityRequest facilityRequest = new FacilityServiceClient.FacilityRequest();
                        facilityRequest.CurrentClient = new FacilityServiceClient.Client();
                        facilityRequest.CurrentClient.ClientId = Convert.ToInt32(txtClientId.Text);
                        facilityRequest.CurrentClient.ClientName =txtClientName.Text;
                        facilityRequest.FacilityAmount = Convert.ToDecimal(txtFacilityAmount.Text);
                       
                        facilityRequest.FaciliytId = 12;
                        facilityRequest.FaciliytType = cmbFacilityType.SelectedItem.ToString();

                        FacilityServiceClient.FacilityResponse facilityResponse = new FacilityServiceClient.FacilityResponse();
                        facilityResponse = client.CreateFacility(facilityRequest);

                        if (facilityResponse != null)
                        {
                            StringBuilder strResult = new StringBuilder();
                            strResult.AppendLine("Client Id: ");
                            strResult.AppendLine(facilityResponse.CurrentClient.ClientId.ToString());
                            strResult.AppendLine(Environment.NewLine);
                            strResult.AppendLine("Client Name: ");
                            strResult.AppendLine(facilityResponse.CurrentClient.ClientName);
                            strResult.AppendLine(Environment.NewLine);
                            strResult.AppendLine("Client Salaray: ");
                            strResult.AppendLine(facilityResponse.ClientSalaryAmount != null ? facilityResponse.ClientSalaryAmount.ToString() : string.Empty);
                            strResult.AppendLine(Environment.NewLine);
                            strResult.AppendLine("Loan Status: ");
                            strResult.AppendLine(facilityResponse.LoanStatus != null ? facilityResponse.LoanStatus : string.Empty);
                            strResult.AppendLine(Environment.NewLine);


                            txtResult.Text = strResult.ToString();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("Please enter all mandatory fields","Validation");
            }

        }

        /// <summary>
        /// Validation
        /// </summary>
        /// <returns></returns>
        private bool Validate()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(txtClientId.Text) || string.IsNullOrEmpty(txtClientName.Text) ||
                string.IsNullOrEmpty(txtFacilityAmount.Text) || cmbFacilityType.SelectedItem == null ||
                cmbTenant.SelectedItem == null)
            {
                isValid = false;
            }

           

            return isValid;
        }

        private void Client_Load(object sender, EventArgs e)
        {
            this.txtClientId.Validating += new CancelEventHandler(txtClientId_Validating);
            this.txtFacilityAmount.Validating += new CancelEventHandler(txtFacilityAmount_Validating);
        }

        void txtFacilityAmount_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = false;

            decimal iOutPut = 0;
            if (!Decimal.TryParse(txtFacilityAmount.Text, out iOutPut))
            {
                MessageBox.Show("Invalid Input: Facility Amount", "Error");
                e.Cancel = true;
            }
        }

        void txtClientId_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = false;

            int iOutPut = 0;
            if (!Int32.TryParse(txtClientId.Text, out iOutPut))
            {
                MessageBox.Show("Invalid Input: ClientId","Error");
                e.Cancel = true;
            }
        }
    }
}
