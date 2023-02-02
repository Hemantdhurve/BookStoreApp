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
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistBL iwishlistBL;

        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;

        public WishlistController(IWishlistBL iwishlistBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.iwishlistBL = iwishlistBL;
            //Added redis catch
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }
        [Authorize]
        [HttpPost]
        [Route("Add")]
        public IActionResult AddWishlist(long bookId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = iwishlistBL.AddWishlist(userId, bookId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Book added to Wishlist", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book Not Added to Wishlist" });
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
        public IActionResult DeleteWishlist(long wishlistId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = iwishlistBL.DeleteWishlist(wishlistId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Wishlist Deleted Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Wishlist deletion UnSuccessful" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("RetriveAll")]
        public IActionResult RetriveWishlist()
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = iwishlistBL.RetriveWishlist(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Wishlist Retrived Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Wishlist Retrival UnSuccessful" });
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

            var cacheKey = "wishlistList";
            string serializedWishlistList;
            var wishlistList = new List<WishlistModel>();
            var redisWishlistList = await distributedCache.GetAsync(cacheKey);
            if (redisWishlistList != null)
            {
                serializedWishlistList = Encoding.UTF8.GetString(redisWishlistList);
                wishlistList = JsonConvert.DeserializeObject<List<WishlistModel>>(serializedWishlistList);
            }
            else
            {
                wishlistList = iwishlistBL.RetriveWishlist(userId).ToList();
                //bookList = GetFromDb();
                serializedWishlistList = JsonConvert.SerializeObject(wishlistList);
                redisWishlistList = Encoding.UTF8.GetBytes(serializedWishlistList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisWishlistList, options);
            }
            return Ok(wishlistList);
        }
    }
}
