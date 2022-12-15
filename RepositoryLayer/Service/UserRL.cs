using CommonLayer.Modal;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL:IUserRL
    {
        private readonly IConfiguration iconfiguration;
        public UserRL(IConfiguration iconfiguration)
        {
                this.iconfiguration= iconfiguration;
        }
        
        public UserRegistrationModel Registration(UserRegistrationModel userRegistrationModel)
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreApp;Integrated Security=True;");

            try
            {
                //using block has a self closing connection property 
                using(con)
                {
                    SqlCommand cmd = new SqlCommand("SPRegistration", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FullName", userRegistrationModel.FullName);
                    cmd.Parameters.AddWithValue("@EmailId", userRegistrationModel.EmailId);
                    cmd.Parameters.AddWithValue("@Password", userRegistrationModel.Password);
                    cmd.Parameters.AddWithValue("@MobileNumber", userRegistrationModel.MobileNumber);

                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result != 0)
                    {
                        return userRegistrationModel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
