using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL iadminBL;

        public AdminController(IAdminBL iadminBL)
        {
                this.iadminBL=iadminBL;
        }
        
        [HttpPost]
        [Route("AdminLogin")]
        public IActionResult AdminLogin(AdminLoginModel admin)
        {
            try
            {
                var result = iadminBL.AdminLogin(admin);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Admin Login Successful", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Admin Login UnSuccessful" });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
