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
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackBL ifeedbackBL;

        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;

        public FeedbackController(IFeedbackBL ifeedbackBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.ifeedbackBL = ifeedbackBL;
            //Added redis catch
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        [Authorize]
        [HttpPost]
        [Route("Add")]
        public IActionResult AddFeedback(FeedbackModel feedbackModel)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = ifeedbackBL.AddFeedback(userId, feedbackModel);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Feedback Added Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Feedback Addition UnSuccessfully" });
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
        public IActionResult RetriveFeedback(long bookId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = ifeedbackBL.RetriveFeedback(bookId);

                if (result != null)
                {
                    return Ok(new { Success = true, Message = "Feedback Retrive Successful", Data = result });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Feedback Retrival Unsuccessful " });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("Redis")]
        public async Task<IActionResult> GetAllFeedbackUsingRedisCache()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

            var cacheKey = "feedbackList";
            string serializedFeedbackList;
            var feedbackList = new List<FeedbackModel>();
            var redisFeedbackList = await distributedCache.GetAsync(cacheKey);
            if (redisFeedbackList != null)
            {
                serializedFeedbackList = Encoding.UTF8.GetString(redisFeedbackList);
                feedbackList = JsonConvert.DeserializeObject<List<FeedbackModel>>(serializedFeedbackList);
            }
            else
            {
                feedbackList = ifeedbackBL.RetriveFeedback(userId).ToList();
                //bookList = GetFromDb();
                serializedFeedbackList = JsonConvert.SerializeObject(feedbackList);
                redisFeedbackList = Encoding.UTF8.GetBytes(serializedFeedbackList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisFeedbackList, options);
            }
            return Ok(feedbackList);
        }
    }
}
