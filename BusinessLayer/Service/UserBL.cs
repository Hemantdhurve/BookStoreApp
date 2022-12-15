using BusinessLayer.Interface;
using CommonLayer.Modal;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL:IUserBL
    {
        private readonly IUserRL iuserRL;
        public UserBL(IUserRL iuserRL)
        {
                this.iuserRL = iuserRL;
        }
        public UserRegistrationModel Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                return iuserRL.Registration(userRegistrationModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                return iuserRL.Login(userLoginModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string ForgetPassword(string emailId)
        {
            try
            {
                return iuserRL.ForgetPassword(emailId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string ResetPassword(string emailId, string newPassword, string confirmPassword)
        {
            try
            {
                return iuserRL.ResetPassword(emailId, newPassword, confirmPassword);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
