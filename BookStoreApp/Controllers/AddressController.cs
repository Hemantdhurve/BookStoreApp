using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
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
    public class AddressController : ControllerBase
    {
        public readonly IAddressBL iaddressBL;

        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public AddressController(IAddressBL iaddressBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.iaddressBL= iaddressBL;
            //Added redis catch
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        [Authorize]
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

        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteAddress(long addressId)
        {
            try
            {
                var result = iaddressBL.DeleteAddress(addressId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Address Deleted Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Address deletion UnSuccessful" });
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
        public IActionResult RetriveAddress()
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iaddressBL.RetriveAddress(userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Address Retrive Successful", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Address Retrival Unsuccessful" });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateAddress(long addressId,Addressmodel addressmodel)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iaddressBL.UpdateAddress(userId,addressId, addressmodel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Address Updated Successful", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Address Updation Unsuccessful" });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpGet]
        [Route("Redis")]
        public async Task<IActionResult> GetAllAddressUsingRedisCache()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

            var cacheKey = "addressList";
            string serializedAddressList;
            var addressList = new List<Addressmodel>();
            var redisAddressList = await distributedCache.GetAsync(cacheKey);
            if (redisAddressList != null)
            {
                serializedAddressList = Encoding.UTF8.GetString(redisAddressList);
                addressList = JsonConvert.DeserializeObject<List<Addressmodel>>(serializedAddressList);
            }
            else
            {
                addressList = iaddressBL.RetriveAddress(userId).ToList();
                //bookList = GetFromDb();
                serializedAddressList = JsonConvert.SerializeObject(addressList);
                redisAddressList = Encoding.UTF8.GetBytes(serializedAddressList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisAddressList, options);
            }
            return Ok(addressList);
        }

    }
}
