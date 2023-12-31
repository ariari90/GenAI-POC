using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Activities;
using System.Web;
using System.Collections;
using DataContractLibrary;
using System.ServiceModel;

namespace DecisionLibrary
{
    public class MetaDataFetch
    {

        public string AssemblyInvoke { get; set; }

        public string NamespaceClassNameInvoke { get; set; }

        string connectionString;

        public void GetConnection()
        {
            ConnectionStringSettingsCollection connectionStringSettingsCollection = ConfigurationManager.ConnectionStrings;

            foreach (ConnectionStringSettings connectionStringSettings in connectionStringSettingsCollection)
            {
                if (string.CompareOrdinal(connectionStringSettings.Name, "RuleSetStoreConnectionString") == 0)
                    connectionString = connectionStringSettings.ConnectionString;
            }

            if (connectionString == null)
                throw new ConfigurationErrorsException("SQL connection string not available for the (should be provided in the config file).");
        }

        public void GetProcessName(int tenantId, string workflowName)
        {
            string[] results = new string[2];
            SqlConnection sqlConn = null;

            try
            {
                GetConnection();

                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();

                SqlCommand command = new SqlCommand("GetAssemblyNamespaceClassDetails", sqlConn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@tenantId", System.Data.SqlDbType.Int);
                command.Parameters.Add("@className", System.Data.SqlDbType.NVarChar);
                command.Parameters["@tenantId"].Value = tenantId;
                command.Parameters["@className"].Value = workflowName;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    try
                    {
                        results[0] = reader.GetValue(0).ToString();
                        results[1] = reader.GetValue(0).ToString() + "." + workflowName;

                        AssemblyInvoke = results[0];
                        NamespaceClassNameInvoke = results[1];
                    }
                    catch (InvalidCastException)
                    {

                    }
                }

                sqlConn.Close();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (sqlConn != null)
                    sqlConn.Close();
            }
            //return results;
        }
    }
}
