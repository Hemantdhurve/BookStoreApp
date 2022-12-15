using BusinessLayer.Interface;
using CommonLayer.Modal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
