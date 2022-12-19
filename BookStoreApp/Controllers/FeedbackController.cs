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
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackBL ifeedbackBL;

        public FeedbackController(IFeedbackBL ifeedbackBL)
        {
            this.ifeedbackBL = ifeedbackBL;
        }

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
                    return this.Ok(new { Status = true, Message = "Addition of Book is successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book addition Unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
