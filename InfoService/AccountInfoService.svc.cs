using DataContractLibrary;
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
    public class AccountInfoService : IAccountInfoService
    {
        string connectionString;

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
                SqlCommand cmd = new SqlCommand("select uniqueId, schemeId, schemeName, fundManagerName, percantageContribution, createdDate, exitDate, isPreferred from SchemeInfo where uniqueId=" + uniqueId, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                schemas = MapList<SchemeInfo>(reader);
            }
            return schemas;
        }

        public SchemeInfo GetSchemePreference(int uniqueId)
        {
            IAccountInfoService infoService = new InfoService.AccountInfoService();
            var schemes = infoService.GetCurrentSchemeDetails(uniqueId);
            var preferredScheme = schemes.FirstOrDefault(x => x.IsPreferred == true);
            return preferredScheme;
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
                    var typeCheck = reader[prop.Name].GetType();
                    if (typeCheck.Name != "DBNull")
                    {
                        prop.SetValue(obj, Convert.ChangeType(reader[prop.Name], propType), null);
                    }
                    else
                    {
                        prop.SetValue(obj, null, null);
                    }
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
                    var typeCheck = reader[prop.Name].GetType();
                    if (typeCheck.Name != "DBNull")
                    {
                        prop.SetValue(obj, Convert.ChangeType(reader[prop.Name], propType), null);
                    }
                    else
                    {
                        prop.SetValue(obj, null, null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }

    }
}
