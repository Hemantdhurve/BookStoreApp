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
    public class CartRL : ICartRL
    {
        private readonly IConfiguration iconfiguration;

        public CartRL(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        public SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreApp;Integrated Security=True;");

        public CartModel cartModel=new CartModel();
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
                    if (result != 0)
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
        public IEnumerable<CartModel> RetriveCart(long userId)
        {
            List<CartModel> cartList = new List<CartModel>();
            try
            {
                using (con)
                {

                    SqlCommand cmd = new SqlCommand("SPRetriveAllCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    con.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            cartList.Add(new CartModel()
                            {
                                CartId = Convert.ToInt64(dataReader["CartId"]),
                                BookId = Convert.ToInt64(dataReader["BookId"]),
                                UserId = Convert.ToInt64(dataReader["UserId"]),
                                BookQuantity = Convert.ToInt64(dataReader["BookQuantity"])
                            });
                        }
                        return cartList;
                    }
                    else
                    {
                        return null;
                    }
                    
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string UpdateCartQty(long cartId,long bookQuantity)
        {
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPUpdateQTY", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CartId", cartId);
                    cmd.Parameters.AddWithValue("@BookQuantity", bookQuantity);
                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    if(result != 0)
                    {
                        return "Updated quantity of book: " + bookQuantity.ToString();
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
