using CommonLayer.Modal;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public UserRegistrationModel Registration(UserRegistrationModel userRegistrationModel);
    }
}
