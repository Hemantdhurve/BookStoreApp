using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        public readonly IAddressBL iaddressBL;
        public AddressController(IAddressBL iaddressBL)
        {
            this.iaddressBL= iaddressBL;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult AddAddress(Addressmodel addressmodel)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = iaddressBL.AddAddress(userId,addressmodel);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Address Added Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Address addition Unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
