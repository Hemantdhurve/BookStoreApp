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
    public class OrderRL:IOrderRL
    {
        private readonly IConfiguration iconfiguration;
        public OrderRL(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }

        public SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreApp;Integrated Security=True;");

        public OrderModel AddOrder(long userId,OrderModel orderModel)
        {
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPAddOrder", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@BookId", orderModel.BookId);
                    cmd.Parameters.AddWithValue("@CartId", orderModel.CartId);
                    cmd.Parameters.AddWithValue("@AddressId", orderModel.AddressId);
                    con.Open();

                    var result = cmd.ExecuteNonQuery();
                    if(result != 0)
                    {
                        return orderModel;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
