using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace InfoService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class AccountBanking : IAccountBanking
    {
        string connectionString = @"Data Source=DESKTOP-2PDJ9M3; Database=gen_ai_poc; Initial Catalog=gen_ai_poc; Integrated Security=True";
        public List<HoldingSummaryData> GetHoldingSummary(int uid)
        {
            List<HoldingSummaryData> hslists = null;

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("select Uniqueid,SchemeName,TotalUnits,Nav,Amount, CreatedDate, ExitDate from HoldingSummary", cn);
                SqlDataReader dataReader = cmd.ExecuteReader();
                hslists = GetList<HoldingSummaryData>(dataReader);
            }

            //hslists = hslists.Where(x => x.Uniqueid == uid || (x.TransactionDate <= startdate && x.TransactionDate >= enddate)).ToList();

            return hslists.ToList();
        }

        public List<UserContributionData> GetUserContribution(int uid, DateTime startdate, DateTime enddate)
        {
            List<UserContributionData> userContributionLists = null;

            if (uid <= 0 || string.IsNullOrEmpty(Convert.ToString(startdate)) || string.IsNullOrEmpty(Convert.ToString(enddate)))
            {
                throw new FaultException("Input values are not valid");
            }

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("select UniqueId,TransactionDate,Description,Amount,TransactionType from TransactionSummary", cn);
                var dataReader = cmd.ExecuteReader();
                userContributionLists = GetList<UserContributionData>(dataReader);
            }

            userContributionLists = userContributionLists.Where(x => x.Uniqueid == uid || (x.TransactionDate >= startdate && x.TransactionDate <= enddate)).ToList();

            return userContributionLists.ToList();
        }

        public ValidationResponse UpdatePersonalDetails(PersonalDetails personDetails)
        {
            ValidationResponse response = new ValidationResponse();
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                SqlCommand cmdselect = new SqlCommand("select * from UserAccount where UniqueId=" + personDetails.Uniqueid, con);
                var reader = cmdselect.ExecuteReader();

                if (reader.HasRows)
                {
                    SqlCommand cmd = new SqlCommand("update UserAccount set Address1=@Address1,Address2=@Address2,City=@City, PinCode=@PinCode, Mobile=@Mobile where UniqueId=@UniqueId", con);
                    cmd.Parameters.AddWithValue("@Address1", personDetails.Address1);
                    cmd.Parameters.AddWithValue("@Address2", personDetails.Address2);
                    cmd.Parameters.AddWithValue("@City", personDetails.City);
                    cmd.Parameters.AddWithValue("@PinCode", personDetails.PinCode);
                    cmd.Parameters.AddWithValue("@Mobile", personDetails.Mobile);
                    cmd.Parameters.AddWithValue("@UniqueId", personDetails.Uniqueid);
                    cmd.ExecuteNonQuery();
                    response.Status = "Success";
                }
                else
                {
                    response.Status = "Reject";
                }
                reader.Close();
                con.Close();

            }
            catch (Exception e)
            {
                response.Status = "Failure";
                throw new FaultException(e.StackTrace);
            }
            return response;
        }

        public List<T> GetList<T>(SqlDataReader reader)
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
