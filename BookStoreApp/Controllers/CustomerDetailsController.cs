using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Linq;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailsController : ControllerBase
    {
        public readonly ICustomerDetailsBL icustomerDetailsBL;
        public CustomerDetailsController(ICustomerDetailsBL icustomerDetailsBL)
        {
            this.icustomerDetailsBL = icustomerDetailsBL;
        }

        [Authorize]
        [HttpGet]
        [Route("Retrive")]
        public IActionResult RetriveDetails()
        {
            try
            {
                var userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result=icustomerDetailsBL.RetriveDetails(userId);
                if(result != null)
                {
                    return this.Ok(new { Status = true, Message = "Retrived Customer Details Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Retrival Customer Details Unsuccessful" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }
    }
}
