using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        string webToken = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

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
                this.webToken = token;
            }
            catch(Exception)
            {
                this.token.Text = "{Authentication Failed}";
            }
            
        }
    }
}