using CommonLayer.Modal;
using CommonLayer.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL:IUserRL
    {
        private readonly IConfiguration iconfiguration;
        public static string Key = "hemanT15893@asd";
        public UserRL(IConfiguration iconfiguration)
        {
                this.iconfiguration= iconfiguration;
        }
        public SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreApp;Integrated Security=True;");

        public UserRegistrationModel Registration(UserRegistrationModel userRegistrationModel)
        {

            try
            {
                //using block has a self closing connection property 
                using(con)
                {
                    SqlCommand cmd = new SqlCommand("SPRegistration", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FullName", userRegistrationModel.FullName);
                    cmd.Parameters.AddWithValue("@EmailId", userRegistrationModel.EmailId);
                    cmd.Parameters.AddWithValue("@Password", ConvertToEncrypt(userRegistrationModel.Password));
                    cmd.Parameters.AddWithValue("@MobileNumber", userRegistrationModel.MobileNumber);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result != 0)
                    {
                        return userRegistrationModel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                using (con)
                {
                    SqlCommand cmd = new SqlCommand("SPLogin", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId", userLoginModel.EmailId);
                    cmd.Parameters.AddWithValue("@Password", ConvertToEncrypt(userLoginModel.Password));
                    //var decryptPass = ConvertToDecrypt(pass.Password);
                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                   
                    if (result != 0)
                    {
                        var token = GenerateSecurityToken(userLoginModel.EmailId);
                        return token;
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

        public string GenerateSecurityToken(string emailId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                { 
                    //Added Role claim for the user
                    new Claim(ClaimTypes.Role,"User"),
                    new Claim("EmailId", emailId)
                    
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string ConvertToEncrypt(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    return "";
                }

                password += Key;

                var passwordBytes = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(passwordBytes);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public static string ConvertToDecrypt(string base64EncodeData)
        {
            try
            {
                if (string.IsNullOrEmpty(base64EncodeData))
                {
                    return "";
                }
                   
                var base64EncodeBytes = Convert.FromBase64String(base64EncodeData);

                var result = Encoding.UTF8.GetString(base64EncodeBytes);
                result = result.Substring(0, result.Length - Key.Length);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        public string ForgetPassword(string emailId)
        {
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPForgotPass", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId", emailId);
                    con.Open();

                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            //emailId = Convert.ToString(dataReader["EmailId"] == DBNull.Value ? default : dataReader["EmailId"]);
                            if(dataReader["EmailId"] == DBNull.Value)
                            {
                                return default;
                            }
                            else
                            {
                                emailId = Convert.ToString(dataReader["EmailId"]);
                                return emailId;
                            }
                        }
                        var token = this.GenerateSecurityToken(emailId);
                        MSMQ msmq = new MSMQ();
                        msmq.sendData2Queue(token);

                        return token;
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

        public string ResetPassword(string emailId, string newPassword, string confirmPassword)
        {
            using (con)
            {
                try
                {
                    if(newPassword.Equals(confirmPassword))
                    {

                        SqlCommand cmd = new SqlCommand("SPResetPassword", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmailId", emailId);
                        cmd.Parameters.AddWithValue("@Password", ConvertToEncrypt(newPassword));
                        con.Open();

                        SqlDataReader dataReader=cmd.ExecuteReader();

                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                // DBNull.Value can be used to explicitly assign a nonexistent value to a database field
                                emailId = Convert.ToString(dataReader["EmailId"] == DBNull.Value ? default : dataReader["EmailId"]);
                                newPassword = Convert.ToString(dataReader["Password"] == DBNull.Value ? default : dataReader["Password"]);
                            }
                            return emailId;
                        }
                        return emailId;

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
