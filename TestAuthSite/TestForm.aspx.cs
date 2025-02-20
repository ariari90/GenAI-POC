using Common;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TestAuthSite
{
    public partial class TestForm : System.Web.UI.Page
    {
        public static AggregatorResponse response { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static AggregatorResponse GetResponse()
        {
            return response;
        }

        protected void Authenticate_Btn_Click(object sender, EventArgs e)
        {
            string username = this.username.Text;
            string password = this.password.Text;
            AuthInfo authInfo = new AuthInfo()
            { UserName = username, Password = password };
            var authUrl = new Uri("http://localhost:54413/AggregatorAuthService.svc/GetToken");
            WebRequest authWebRequest = WebRequest.Create(authUrl);
            string stringData = String.Empty;
            using (var stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(typeof(AuthInfo));
                serializer.Serialize(stringwriter, authInfo);
                stringData = stringwriter.ToString();
            }
            ASCIIEncoding encoding = new ASCIIEncoding();
            var data = encoding.GetBytes(stringData);

            authWebRequest.Method = "POST";
            authWebRequest.ContentType = "application/x-www-form-urlencoded";
            authWebRequest.ContentLength = data.Length;

            Stream newStream = authWebRequest.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            WebResponse authResponse = null;
            try
            {
                authResponse = authWebRequest.GetResponse();
                StreamReader stream = new StreamReader(authResponse.GetResponseStream());
                string tokenResponse = stream.ReadToEnd();

                string token = XElement.Parse(tokenResponse).Value;
                this.token.Text = token;
            }
            catch(Exception)
            {
                this.token.Text = "{Authentication Failed}";
            }
            
        }

        protected void GetDataBtn_Click(object sender, EventArgs e)
        {
            DetailsView1.Visible = false;
            AggregatorRequest request = new AggregatorRequest();
            RequestType requestType = new RequestType();
            switch(RequestTypeCombo.SelectedValue)
            {
                case "AccountInfo":
                    requestType = RequestType.AccountInfo;
                    break;
                case "HoldingsInfo":
                    requestType = RequestType.HoldingsInfo;
                    break;
                case "UpdateDetails":
                    requestType = RequestType.UpdateDetails;
                    break;
                case "Transaction":
                    requestType = RequestType.Transaction;
                    break;
                case "AccountAndHoldings":
                    requestType = RequestType.AccountAndHoldings;
                    break;
                case "Extra":
                    requestType = RequestType.Extra;
                    break;
            }
            request.RequestType = requestType;
            try
            {
                request.UniqueId = Convert.ToInt32(this.UniqueId.Text);
            }
            catch (Exception)
            {
                ErrorMessageLabel.Text = "UniqueID must be an interger.";
                return;
            }

            ASCIIEncoding encoding = new ASCIIEncoding();
            var uri = new Uri("http://localhost:54413/AggregatorAuthService.svc/GetData");

            WebRequest webRequest = WebRequest.Create(uri);
            string stringData = String.Empty;
            using (var stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(typeof(AggregatorRequest));
                serializer.Serialize(stringwriter, request);
                stringData = stringwriter.ToString();
            }
            var data = encoding.GetBytes(stringData);

            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = data.Length;

            webRequest.Headers.Add("Authorization", "Bearer " + this.token.Text);

            ErrorMessageLabel.Text = String.Empty;

            try
            {
                Stream newStream = webRequest.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();

                WebResponse webResponse = webRequest.GetResponse();

                AggregatorResponse response = GetDataFromStream<AggregatorResponse>(webResponse.GetResponseStream());
                AggregatorResponseDataAccess.response = response;
            }
            catch (WebFaultException)
            {
                ErrorMessageLabel.Text = "Error: Unauthorized Access.";
            }
            catch (WebException ex)
            {
                ErrorMessageLabel.Text = ex.Message;
            }
            catch (Exception)
            {
                ErrorMessageLabel.Text = "Error: Could not make service call to AggregatorSvcAuth (are you running the WCF?)";
            }
            
            if (AggregatorResponseDataAccess.response != null && AggregatorResponseDataAccess.response.AccountInfoResponse != null)
            {
                DetailsView1.Visible = true;
                DetailsView1.DataBind();
            }
        }

        private static T GetDataFromStream<T>(Stream data)
        {
            StreamReader reader = new StreamReader(data);
            T response;
            string xmlString = reader.ReadToEnd();

            var doc = XDocument.Parse(xmlString);

            foreach (var element in doc.Descendants())
            {
                element.Attributes().Where(a => a.IsNamespaceDeclaration).Remove();
                element.Name = element.Name.LocalName;
            }
            xmlString = doc.ToString();

            using (var stringReader = new System.IO.StringReader(xmlString))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                response = (T)xmlSerializer.Deserialize(stringReader);
            }
            return response;
        }

        public object BindList<T>(object list)
        {
            if (list == null)
            {
                return new List<T>();
            }

            return list;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView2_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }
    }
}