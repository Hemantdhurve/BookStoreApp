using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailsController : ControllerBase
    {
        public readonly ICustomerDetailsBL icustomerDetailsBL;

        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public CustomerDetailsController(ICustomerDetailsBL icustomerDetailsBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.icustomerDetailsBL = icustomerDetailsBL;
            //Added redis catch
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
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

        [HttpGet]
        [Route("Redis")]
        public async Task<IActionResult> GetAllBookUsingRedisCache()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

            var cacheKey = "customerList";
            string serializedCustomerList;
            var customerList = new List<CustomerDetailsModel>();
            var redisCustomerList = await distributedCache.GetAsync(cacheKey);
            if (redisCustomerList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(redisCustomerList);
                customerList = JsonConvert.DeserializeObject<List<CustomerDetailsModel>>(serializedCustomerList);
            }
            else
            {
                customerList = icustomerDetailsBL.RetriveDetails(userId).ToList();
                //bookList = GetFromDb();
                serializedCustomerList = JsonConvert.SerializeObject(customerList);
                redisCustomerList = Encoding.UTF8.GetBytes(serializedCustomerList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCustomerList, options);
            }
            return Ok(customerList);
        }
    }
}
