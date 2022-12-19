﻿using CommonLayer.Model;
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

        public Addressmodel RetriveAddress(long userId)
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
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            addressmodel.AddressId = Convert.ToInt64(dataReader["AddressId"]);
                            addressmodel.UserId = Convert.ToInt64(dataReader["UserId"]);
                            addressmodel.TypeId = Convert.ToInt64(dataReader["TypeId"]);
                            addressmodel.Address = dataReader["Address"].ToString();
                            addressmodel.City = dataReader["City"].ToString();
                            addressmodel.State = dataReader["State"].ToString();
                        }
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

    }
}