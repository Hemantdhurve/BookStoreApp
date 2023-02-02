using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBL icartBL;

        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public CartController(ICartBL icartBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.icartBL = icartBL;
            //Added redis catch
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
        [HttpPut]
        [Route("UpdateQTY")]
        public IActionResult UpdateCartQty(long cartId, long bookQuantity)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = icartBL.UpdateCartQty(cartId, bookQuantity);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Quantity Updated Successful", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Quantity Updation Unsuccessful" });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteCart")]
        public IActionResult DeleteCart(long cartId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = icartBL.DeleteCart(cartId);
                if (result != null)

                {
                    return this.Ok(new { Success = true, message = "Books Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Books Deletion UnSuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("Redis")]
        public async Task<IActionResult> GetAllCartUsingRedisCache()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

            var cacheKey = "cartList";
            string serializedCartList;
            var cartList = new List<CartModel>();
            var redisCartList = await distributedCache.GetAsync(cacheKey);
            if (redisCartList != null)
            {
                serializedCartList = Encoding.UTF8.GetString(redisCartList);
                cartList = JsonConvert.DeserializeObject<List<CartModel>>(serializedCartList);
            }
            else
            {
                cartList = icartBL.RetriveCart(userId).ToList();
                //bookList = GetFromDb();
                serializedCartList = JsonConvert.SerializeObject(cartList);
                redisCartList = Encoding.UTF8.GetBytes(serializedCartList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCartList, options);
            }
            return Ok(cartList);
        }
    }
}