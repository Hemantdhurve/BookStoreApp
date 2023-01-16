using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CustomerDetailsRL:ICustomerDetailsRL
    {
        public readonly IConfiguration iconfiguration;
        public CustomerDetailsRL(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;         
        }

        public SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreApp;Integrated Security=True;");
        
        public IEnumerable<CustomerDetailsModel> RetriveDetails(long userId)
        {
            List<CustomerDetailsModel> detailsList =  new List<CustomerDetailsModel>();
            using(con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPGetCustDetails",con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    con.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        detailsList.Add(new CustomerDetailsModel()
                        {
                            UserId = Convert.ToInt64(dataReader["UserId"]),
                            FullName = dataReader["FullName"].ToString(),
                            MobileNumber = Convert.ToInt64(dataReader["MobileNumber"]),
                        });
                    }
                    return detailsList;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

    }
}
