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
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistBL iwishlistBL;

        public WishlistController(IWishlistBL iwishlistBL)
        {
            this.iwishlistBL = iwishlistBL;
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
    }
}
