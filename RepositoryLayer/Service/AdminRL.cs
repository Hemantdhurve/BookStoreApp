using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RepositoryLayer.Service
{
    public class AdminRL:IAdminRL
    {
        private readonly IConfiguration iconfiguration;
        public static string Key = "hemanT15893@asd";
        public AdminRL(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        public SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreApp;Integrated Security=True;");

        public string AdminLogin(AdminLoginModel adminLoginModel)
        {
            try
            {
                using (con)
                {
                    SqlCommand cmd = new SqlCommand("SPAdminLogin", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId", adminLoginModel.EmailId);
                    cmd.Parameters.AddWithValue("@Password", adminLoginModel.Password);
                    con.Open();
                    var result = cmd.ExecuteNonQuery();

                    if (result != 0)
                    {
                        var token = GenerateSecurityToken(adminLoginModel.EmailId);
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
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(iconfiguration["JWT:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                         new Claim(ClaimTypes.Role,"Admin"),
                         new Claim(ClaimTypes.Email, emailId)
                    }),

                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}
