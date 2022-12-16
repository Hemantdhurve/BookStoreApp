using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class AdminBL:IAdminBL
    {
        private readonly IAdminRL iadminRL;

        public AdminBL(IAdminRL iadminRL)
        {
            this.iadminRL = iadminRL;
        }

        public string AdminLogin(AdminLoginModel adminLoginModel)
        {
            try
            {
                return iadminRL.AdminLogin(adminLoginModel);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
