using System;
using System.Collections.Generic;
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
    public class Service1 : IService1
    {
        string connectionString = @"Data Source=DESKTOP-2PDJ9M3; Initial Catelog=bankDb; Persist Security=True; Integrated Security=True; MultipleResultSets=True";
        public PersonalInfo ViewPersonalInfo(int uniqueId)
        {
            PersonalInfo personalInfo = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select uniqueId, name, fathersName, mothersName, dateOfBirth, gender, nationality, isKycDone, " +
                            "address1, address2, city, pinCode, mobile from PersonalInfo");
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
                SqlCommand cmd = new SqlCommand("select accountNumber, bankBranch, bankName, bankAddress, ifscCode from PersonalInfo");
                SqlDataReader reader = cmd.ExecuteReader();
                bankInfo = MapValue<BankInfo>(reader);
            }
            return bankInfo;
        }



        public T MapValue<T>(IDataReader reader)
        {
            Type type = typeof(T);
            T obj = (T)Activator.CreateInstance(type);

            reader.Read();
            if(reader != null)
            {
                
                foreach (var prop in type.GetProperties())
                {
                    var propType = prop.PropertyType;
                    prop.SetValue(obj, Convert.ChangeType(reader[prop.Name].ToString(), propType));
                }
            }

            return obj;
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
                    prop.SetValue(obj, Convert.ChangeType(reader[prop.Name].ToString(), propType));
                }
                list.Add(obj);
            }
            return list;
        }

    }
}
