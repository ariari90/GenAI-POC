using Common.Entities;
using DataContractLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace InfoService
{
    public class AccountBankingService : IAccountBankingService
    {
        string _connectionString;
        int _amountPerUnits = 10;

        public AccountBankingService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;
        }
        public HoldingSummaryResponse GetHoldingSummary(int uid)
        {
            HoldingSummaryResponse response = new HoldingSummaryResponse();

            List<HoldingSummaryData> hslists = null;
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("select * from HoldingSummary", cn);
                SqlDataReader dataReader = cmd.ExecuteReader();
                hslists = GetList<HoldingSummaryData>(dataReader);
            }

            response.HoldingSummaryData = hslists;
            response.TotalAmount = hslists.Sum(x => x.Amount);

            //hslists = hslists.Where(x => x.Uniqueid == uid || (x.TransactionDate <= startdate && x.TransactionDate >= enddate)).ToList();

            return response;
        }

        public List<UserContributionData> GetUserContribution(int uid, DateTime startdate, DateTime enddate)
        {
            List<UserContributionData> userContributionLists = null;

            if (uid <= 0 || string.IsNullOrEmpty(Convert.ToString(startdate)) || string.IsNullOrEmpty(Convert.ToString(enddate)))
            {
                throw new FaultException("Input values are not valid");
            }

            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("select * from TransactionSummary where uniqueId=" + uid, cn);
                var dataReader = cmd.ExecuteReader();
                userContributionLists = GetList<UserContributionData>(dataReader);
            }

            userContributionLists = userContributionLists.Where(x => (x.TransactionDate >= startdate && x.TransactionDate <= enddate)).ToList();

            return userContributionLists.ToList();
        }


        public ValidationResponse UpdatePersonalDetails(PersonalDetails personDetails)
        {
            ValidationResponse response = new ValidationResponse();
            try
            {
                SqlConnection con = new SqlConnection(_connectionString);
                con.Open();

                SqlCommand cmdselect = new SqlCommand("select * from UserAccount where UniqueId=" + personDetails.Uniqueid, con);
                var reader = cmdselect.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Close();
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
                    reader.Close();
                    response.Status = "Reject";
                }

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