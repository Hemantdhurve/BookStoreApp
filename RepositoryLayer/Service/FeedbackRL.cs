using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RepositoryLayer.Service
{
    public class FeedbackRL:IFeedbackRL
    {
        public readonly IConfiguration iconfiguration;
        public FeedbackRL(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        public SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreApp;Integrated Security=True;");
        public FeedbackModel feedbackModel= new FeedbackModel();
        public string AddFeedback(long userId,FeedbackModel feedbackModel)
        {
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPAddFeedback", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserId", userId);
                    cmd.Parameters.AddWithValue("BookId", feedbackModel.BookId);
                    cmd.Parameters.AddWithValue("Rating", feedbackModel.Rating);
                    cmd.Parameters.AddWithValue("Comment", feedbackModel.Comment);
                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    if(result != 0)
                    {
                        return "Feedback Added Successfully";
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

        public IEnumerable<FeedbackModel> RetriveFeedback(long bookId)
        {
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPRetriveFeedback", con);
                    cmd.CommandType=CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    con.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    List<FeedbackModel> feedbackList = new List<FeedbackModel>();
                   
                    if (dataReader.Read())
                    {
                        feedbackList.Add(new FeedbackModel()
                        {
                            FeedbackId = Convert.ToInt64(dataReader["FeedbackId"]),                            
                            UserId = Convert.ToInt64(dataReader["UserId"]),                            
                            BookId = Convert.ToInt64(dataReader["BookId"]),
                            Rating = Convert.ToInt32(dataReader["Rating"]),
                            Comment = dataReader["Comment"].ToString()

                        });
                    }
                    return feedbackList ;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

    }
}
