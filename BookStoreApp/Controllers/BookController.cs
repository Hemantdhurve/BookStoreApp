using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBL ibookBL;

        //Redis cache Implementation
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public BookController(IBookBL ibookBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.ibookBL=ibookBL;

            //Added redis catch
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        [Route("AddBook")]
        public IActionResult AddBook(BookModel bookModel)
        {
            try
            {
                var result = ibookBL.AddBook(bookModel);
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

        //[Authorize]
        //[Authorize(Roles = Role.User)]
        [HttpGet]
        [Route("RetriveById")]
        public IActionResult RetriveBookById(long bookId)
        {
            try
            {
                var result = ibookBL.RetriveBookById(bookId);
                if (result != null)

                {
                    return this.Ok(new { Success = true, message = "Book Details Retrived", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Book Details not Retrived" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[Authorize(Roles = Role.Admin)]
        //[Authorize(Roles = Role.User)]
        [HttpGet]
        [Route("RetriveAll")]
        public IActionResult RetriveAllBooks()
        {
            try
            {
                var result = ibookBL.RetriveAllBooks();
                if (result != null)

                {
                    return this.Ok(new { Success = true, message = "Successfully Retrived All Books", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Books Retrive UnSuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[Authorize]
        [HttpPut]
        [Route("UpdateBook")]
        public IActionResult UpdateBookDetails(long bookId, BookModel bookModel)
        {
            try
            {
                var result = ibookBL.UpdateBookDetails(bookId, bookModel);
                if (result != null)

                {
                    return this.Ok(new { Success = true, message = "Books Details Update Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Books Updation UnSuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[Authorize]
        //[Authorize(Roles = Role.User)]
        [HttpDelete]
        [Route("DeleteBook")]
        public IActionResult DeleteBook(long bookId)
        {
            try
            {
                var result = ibookBL.DeleteBook(bookId);
                if (result != null)

                {
                    return this.Ok(new { Success = true, message = "Books Deleted Successfully"});
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
        public async Task<IActionResult> GetAllBookUsingRedisCache()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

            var cacheKey = "bookList";
            string serializedBookList;
            var bookList = new List<BookModel>();
            var redisBookList = await distributedCache.GetAsync(cacheKey);
            if (redisBookList != null)
            {
                serializedBookList = Encoding.UTF8.GetString(redisBookList);
                bookList = JsonConvert.DeserializeObject<List<BookModel>>(serializedBookList);
            }
            else
            {
                bookList = ibookBL.RetriveAllBooks().ToList() ;
                //bookList = GetFromDb();
                serializedBookList = JsonConvert.SerializeObject(bookList);
                redisBookList = Encoding.UTF8.GetBytes(serializedBookList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisBookList, options);
            }
            return Ok(bookList);
        }

        [HttpPut]
        [Route("ImageBook")]
        public IActionResult ImageBooks(IFormFile image, long bookId)
        {
            try
            {
                //long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = ibookBL.ImageBooks(image,bookId);
                if (result != null)

                {
                    return this.Ok(new { Success = true, message = "Books Image Uploaded Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Books Image Upload UnSuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
