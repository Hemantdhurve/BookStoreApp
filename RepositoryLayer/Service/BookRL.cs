using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace RepositoryLayer.Service
{
    public class BookRL:IBookRL
    {
        private readonly IConfiguration iconfiguration;

        public BookRL(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        public SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreApp;Integrated Security=True;");

        public BookModel AddBook(BookModel bookModel)
        {
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPBookAddition", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookTitle", bookModel.BookTitle);
                    cmd.Parameters.AddWithValue("@Author", bookModel.Author);
                    cmd.Parameters.AddWithValue("@Rating", bookModel.Rating);
                    cmd.Parameters.AddWithValue("@RatedCount", bookModel.RatedCount);
                    cmd.Parameters.AddWithValue("@DiscountedPrice", bookModel.DiscountedPrice);
                    cmd.Parameters.AddWithValue("@ActualPrice", bookModel.ActualPrice);
                    cmd.Parameters.AddWithValue("@Description", bookModel.Description);
                    cmd.Parameters.AddWithValue("@BookQuantity", bookModel.BookQuantity);
                    cmd.Parameters.AddWithValue("@Image", bookModel.Image);
                    con.Open();

                    var result = cmd.ExecuteNonQuery();
                    if (result != 0)
                    {
                        return bookModel;
                    }
                    else
                    {
                        return null;
                    }

                }
                catch (Exception e)
                {

                    throw e;
                }
            }

        }
    }
}
