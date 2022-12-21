using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackBL ifeedbackBL;

        public FeedbackController(IFeedbackBL ifeedbackBL)
        {
            this.ifeedbackBL = ifeedbackBL;
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
    }
}
