using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBL icartBL;
        public CartController(ICartBL icartBL)
        {
            this.icartBL = icartBL;
        }

        //[Authorize]
        [HttpPost]
        [Route("AddCart")]
        public IActionResult AddCart(CartModel cartModel)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = icartBL.AddCart(cartModel, userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Add to Cart Successful", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Add to Cart Unsuccessful" });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpGet]
        [Route("RetriveCart")]
        public IActionResult RetriveCart()
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = icartBL.RetriveCart(userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Retrive Cart Successful", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Retrive Cart Unsuccessful" });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
