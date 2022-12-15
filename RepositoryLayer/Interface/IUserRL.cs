using CommonLayer.Modal;
using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserRegistrationModel Registration(UserRegistrationModel userRegistrationModel);
        public string Login(UserLoginModel userLoginModel);
        public string ForgetPassword(string emailId);
    }
}
