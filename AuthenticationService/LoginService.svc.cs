using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Common;
using System.Configuration;
using System.Data.SqlClient;
using Common.Entities;

namespace AuthenticationService
{
    public class LoginService : ILoginService
    {
        string _connectionString;

        public LoginService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;
        }

        public bool Authenticate(AuthInfo authInfo)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select userName from UserAuth where userName=@username and password=@password", conn);
                cmd.Parameters.AddWithValue("@username", authInfo.UserName);
                cmd.Parameters.AddWithValue("@password", authInfo.Password);
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
