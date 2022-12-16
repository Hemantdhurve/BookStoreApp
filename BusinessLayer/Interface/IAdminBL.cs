using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAdminBL
    {
        public string AdminLogin(AdminLoginModel adminLoginModel);
    }
}
