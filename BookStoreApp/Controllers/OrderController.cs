using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
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
    public class OrderController : ControllerBase
    {
        public readonly IOrderBL iorderBL;

        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public OrderController(IOrderBL iorderBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.iorderBL = iorderBL;
            //Added redis catch
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        [Authorize]
        [HttpPost]
        [Route("Add")]
        public IActionResult AddOrder(OrderModel orderModel)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iorderBL.AddOrder(userId, orderModel);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Order Added Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Order Addition UnSuccessfully" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteOrder(long orderId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iorderBL.DeleteOrder(orderId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Order Deleted Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Order Deletion UnSuccessfully" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Retrive")]
        public IActionResult RetriveOrder()
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iorderBL.RetriveOrder(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Order Retrive Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Order Retrival UnSuccessfully" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("Redis")]
        public async Task<IActionResult> GetAllOrdersUsingRedisCache()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

            var cacheKey = "orderList";
            string serializedOrderList;
            var orderList = new List<OrderModel>();
            var redisOrderList = await distributedCache.GetAsync(cacheKey);
            if (redisOrderList != null)
            {
                serializedOrderList = Encoding.UTF8.GetString(redisOrderList);
                orderList = JsonConvert.DeserializeObject<List<OrderModel>>(serializedOrderList);
            }
            else
            {
                orderList = iorderBL.RetriveOrder(userId).ToList();
                //bookList = GetFromDb();
                serializedOrderList = JsonConvert.SerializeObject(orderList);
                redisOrderList = Encoding.UTF8.GetBytes(serializedOrderList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisOrderList, options);
            }
            return Ok(orderList);
        }
    }
}
