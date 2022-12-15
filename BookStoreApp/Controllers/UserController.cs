using BusinessLayer.Interface;
using CommonLayer.Modal;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL iuserBL;
        public UserController(IUserBL iuserBL)
        {
            this.iuserBL = iuserBL;       
        }

        [HttpPost]
        [Route ("Register")]
        public IActionResult Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var result = iuserBL.Registration(userRegistrationModel);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Registration is Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration is UnSuccessful" });
                }

            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(UserLoginModel userLoginModel)
        {
            try
            {
                var result = iuserBL.Login(userLoginModel);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Login is Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login is UnSuccessful" });
                }

            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Route("ForgetPassword")]

        public IActionResult ForgetPassword(string emailId)
        {
            try
            {
                var resultLog = iuserBL.ForgetPassword(emailId);

                if (resultLog != null)
                {
                    return Ok(new { success = true, message = "Reset Email Send" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset UnSuccessful" });
                }

            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("ResetPassword")]

        public IActionResult ResetPassword(string newPassword, string confirmPassword)
        {
            try
            {
                var emailId = User.FindFirst("EmailId").Value.ToString();
                var resultLog = iuserBL.ResetPassword(emailId, newPassword, confirmPassword);

                if (resultLog != null)
                {
                    return Ok(new { success = true, message = "Password Reset Successful" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Password Reset UnSuccessful" });
                }

            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
