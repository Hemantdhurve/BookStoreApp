using CommonLayer.Modal;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserRegistrationModel Registration(UserRegistrationModel userRegistrationModel);
    }
}
