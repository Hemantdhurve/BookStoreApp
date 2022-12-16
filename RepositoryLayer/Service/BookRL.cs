using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
        public BookModel bookModel = new BookModel();
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
        public BookModel RetriveBookById(long BookId)
        {
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPRetriveBookById", con);
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", BookId);
                    con.Open();
                    SqlDataReader dataReader= cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while(dataReader.Read())
                        {
                            bookModel.BookId = Convert.ToInt64(dataReader["BookId"]);
                            bookModel.BookTitle = dataReader["BookTitle"].ToString();
                            bookModel.Author= dataReader["Author"].ToString();
                            bookModel.Rating = Convert.ToInt32(dataReader["Rating"]);
                            bookModel.RatedCount = Convert.ToInt64(dataReader["RatedCount"]);
                            bookModel.DiscountedPrice = Convert.ToInt64(dataReader["DiscountedPrice"]);
                            bookModel.ActualPrice = Convert.ToInt64(dataReader["ActualPrice"]);
                            bookModel.Description = dataReader["Description"].ToString();
                            bookModel.Image = dataReader["Image"].ToString();
                        }
                        return bookModel;
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

        public List<BookModel> RetriveAllBooks()
        {
            List<BookModel> bookList = new List<BookModel>();
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPRetriveAllBooks", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            bookList.Add(new BookModel()
                            {
                                BookId = Convert.ToInt64(dataReader["BookId"]),
                                BookTitle = dataReader["BookTitle"].ToString(),
                                Author = dataReader["Author"].ToString(),
                                Rating = Convert.ToInt32(dataReader["Rating"]),
                                RatedCount = Convert.ToInt64(dataReader["RatedCount"]),
                                DiscountedPrice = Convert.ToInt64(dataReader["DiscountedPrice"]),
                                ActualPrice = Convert.ToInt64(dataReader["ActualPrice"]),
                                Description = dataReader["Description"].ToString(),
                                Image = dataReader["Image"].ToString(),
                            });
                        }
                        return bookList;
                    }
                    else
                    {
                        return null;
                    }
                    

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

    }
}
