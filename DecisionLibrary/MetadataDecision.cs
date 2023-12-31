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
    public sealed class MedataDecision : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<int> TenantId { get; set; }

        public InArgument<string> ProcessName { get; set; }

        public OutArgument<string> AssemblyName { get; set; }

        public OutArgument<string> NamespaceClassName { get; set; }

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

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            int tenantId = context.GetValue(this.TenantId);

            ////Caching the TeanantId
            //Hashtable hashTenants = DataContractLibrary.TenantData.InMemory.TenantSessionMap;
            //if (!hashTenants.Contains(context.WorkflowInstanceId))
            //{
            //    hashTenants.Add(context.WorkflowInstanceId, tenantId);
            ////}

            //InstanceContext instanceContext = OperationContext.Current.InstanceContext;
            //AddressableInstanceContextInfo info = instanceContext.Extensions.Find<AddressableInstanceContextInfo>();
            ////return (info != null) ? info.InstanceId : null;

            string processName = context.GetValue(this.ProcessName);
            string[] results = GetProcessName(tenantId, processName);
            context.SetValue(AssemblyName, results[0]);
            context.SetValue(NamespaceClassName, results[1]);
        }


        public string[] GetProcessName(int tenantId, string workflowName)
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
            return results;
        }
    }
}
