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
                    SqlCommand cmd = new SqlCommand("spAddOrder1", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@BookId", orderModel.BookId);
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

        public bool DeleteOrder(long orderId)
        {
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPDeleteOrder", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    if(result != 0 )
                    {
                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public IEnumerable<OrderModel> RetriveOrder(long userId)
        {
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPRetriveOrder", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    con.Open();
                    List<OrderModel> orderList = new List<OrderModel>();
                    SqlDataReader dataReader=cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        orderList.Add(new OrderModel()
                        {
                            OrderId = Convert.ToInt64(dataReader["OrderId"]),
                            UserId = Convert.ToInt64(dataReader["UserId"]),
                            BookId = Convert.ToInt64(dataReader["BookId"]),
                            AddressId = Convert.ToInt64(dataReader["AddressId"]),
                            BookTitle = dataReader["BookTitle"].ToString(),
                            Author = dataReader["Author"].ToString(),
                            Image = dataReader["Image"].ToString(),
                            OrderQuantity = Convert.ToInt64(dataReader["OrderQuantity"]),
                            TotalPrice = Convert.ToInt64(dataReader["TotalPrice"]),
                            TotalDiscountedPrice = Convert.ToInt64(dataReader["TotalDiscountedPrice"]),
                            OrderPlacedDate = Convert.ToDateTime(dataReader["OrderPlacedDate"]),
                        }); 
                    }
                    return orderList;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
