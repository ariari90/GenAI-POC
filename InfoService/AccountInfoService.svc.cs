using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace InfoService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class AccountInfoService : IAccountInfoService
    {
        string connectionString = @"Data Source=DESKTOP-2PDJ9M3; Database=gen_ai_poc; Initial Catalog=gen_ai_poc; Integrated Security=True";
        public AccountInfoService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;
        }
        public PersonalInfo ViewPersonalInfo(int uniqueId)
        {
            PersonalInfo personalInfo = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select uniqueId, name, fathersName, mothersName, dateOfBirth, gender, nationality, isKycDone, " +
                            "address1, address2, city, pinCode, mobile from UserAccount where uniqueId=" + uniqueId, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                personalInfo = MapValue<PersonalInfo>(reader);
            }
            return personalInfo;
        }

        public BankInfo ViewBankInfo(int uniqueId)
        {
            BankInfo bankInfo = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select uniqueId, accountNumber, bankBranch, bankName, address, ifscCode from BankInfo where uniqueId=" + uniqueId, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                bankInfo = MapValue<BankInfo>(reader);
            }
            return bankInfo;
        }

        public List<SchemeInfo> GetCurrentSchemeDetails(int uniqueId)
        {
            List<SchemeInfo> schemas = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select uniqueId, schemaId, schemaName, fundManagerName, percantageContribution from SchemaInfo where uniqueId=" + uniqueId, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                schemas = MapList<SchemeInfo>(reader);
            }
            return schemas;
        }



        public T MapValue<T>(IDataReader reader)
        {
            Type type = typeof(T);
            T obj = (T)Activator.CreateInstance(type);

            
            if(reader.Read())
            {
                foreach (var prop in type.GetProperties())
                {
                    var propType = prop.PropertyType;
                    prop.SetValue(obj, Convert.ChangeType(reader[prop.Name].ToString(), propType), null);
                }
                return obj;
            }

            return default(T);
        }

        public List<T> MapList<T>(IDataReader reader)
        {
            List<T> list = new List<T>();
            while (reader.Read())
            {
                Type type = typeof(T);
                T obj = (T)Activator.CreateInstance(type);
                foreach (var prop in type.GetProperties())
                {
                    var propType = prop.PropertyType;
                    prop.SetValue(obj, Convert.ChangeType(reader[prop.Name].ToString(), propType), null);
                }
                list.Add(obj);
            }
            return list;
        }

    }
}
