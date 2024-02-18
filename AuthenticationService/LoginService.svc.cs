using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Common;
using System.Configuration;
using System.Data.SqlClient;

namespace AuthenticationService
{
    public class LoginService : ILoginService
    {
        string connectionString;

        public LoginService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;
        }

        public bool Authenticate(AuthInfo authInfo)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from UserAuth where userName='" + authInfo.UserName
                    + "' and password='" + authInfo.Password + "'", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
