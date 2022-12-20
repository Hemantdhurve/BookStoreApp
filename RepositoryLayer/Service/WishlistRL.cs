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
    public class WishlistRL : IWishlistRL
    {
        private readonly IConfiguration iconfiguration;

        public WishlistRL(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }

        public SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreApp;Integrated Security=True;");

        public string AddWishlist(long userId, long bookId)
        {
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPAddWishlist", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result != 0)
                    {
                        return "Successfully Added to Wishlist";
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

        public bool DeleteWishlist(long wishlistId)
        {
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPDeleteWishlist", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@WishlistId", wishlistId);
                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result != 0)
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

        public WishlistModel RetriveWishlist(long bookId)
        {
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPRetriveAllWishlist", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    con.Open();
                    WishlistModel wishListModel = new WishlistModel();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            wishListModel.WishlistId = Convert.ToInt32(rd["WishlistId"]);
                            wishListModel.BookId = Convert.ToInt32(rd["BookId"]);
                            wishListModel.UserId = Convert.ToInt32(rd["UserId"]);
                        }
                        return wishListModel;
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
