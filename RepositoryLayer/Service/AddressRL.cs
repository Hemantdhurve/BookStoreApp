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
    public class AddressRL:IAddressRL
    {
        public readonly IConfiguration iconfiguration;
        public AddressRL(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        public SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreApp;Integrated Security=True;");

        public Addressmodel addressmodel = new Addressmodel();
        public Addressmodel AddAddress(long userId,Addressmodel addressmodel)
        {
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPAddAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@TypeId", addressmodel.TypeId);
                    cmd.Parameters.AddWithValue("@Address", addressmodel.Address);
                    cmd.Parameters.AddWithValue("@City", addressmodel.City);
                    cmd.Parameters.AddWithValue("@State", addressmodel.State);
                    con.Open();
                    var result= cmd.ExecuteNonQuery();
                    if (result != 0)
                    {
                        return addressmodel;
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

        public bool DeleteAddress(long addressId)
        {
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPDeleteAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AddressId", addressId);
                    con.Open();
                    var result= cmd.ExecuteNonQuery();
                    if(result != 0)
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

        public IEnumerable<Addressmodel> RetriveAddress(long userId)
        {
            using (con)
            {
                try
                {
                    Addressmodel addressmodel = new Addressmodel();
                    SqlCommand cmd = new SqlCommand("SPRetriveAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    con.Open();
                    List<Addressmodel> addressList = new List<Addressmodel>();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                   while(dataReader.Read())
                    {
                        addressList.Add(new Addressmodel()
                        {
                            AddressId = Convert.ToInt64(dataReader["AddressId"]),
                            UserId = Convert.ToInt64(dataReader["UserId"]),
                            TypeId = Convert.ToInt64(dataReader["TypeId"]),
                            Address = dataReader["Address"].ToString(),
                            City = dataReader["City"].ToString(),
                            State = dataReader["State"].ToString()
                        });
                    }
                    return addressList;

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public Addressmodel UpdateAddress(long userId,long addressId,Addressmodel addressmodel) 
        {
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPUpdateAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AddressId", addressId);
                    cmd.Parameters.AddWithValue("@TypeId", addressmodel.TypeId);
                    cmd.Parameters.AddWithValue("@Address", addressmodel.Address);
                    cmd.Parameters.AddWithValue("@City", addressmodel.City);
                    cmd.Parameters.AddWithValue("@State", addressmodel.State);
                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result != 0)
                    {
                        return addressmodel;
                    }
                    else
                    {
                        return addressmodel;
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
