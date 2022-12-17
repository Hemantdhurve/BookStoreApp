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
    public class CartRL:ICartRL
    {
        private readonly IConfiguration iconfiguration;

        public CartRL(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        public SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreApp;Integrated Security=True;");

        public CartModel AddCart(CartModel cartModel, long userId)
        {
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPCartAdition", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", cartModel.BookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@BookQuantity", cartModel.BookQuantity);
                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    if(result !=0 )
                    {
                        return cartModel;
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
