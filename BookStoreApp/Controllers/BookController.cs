using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Interface;
using System;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBL ibookBL;

        public BookController(IBookBL ibookBL)
        {
            this.ibookBL=ibookBL;
        }

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

        [HttpGet]
        [Route("RetriveById")]
        public IActionResult RetriveBookById(long BookId)
        {
            try
            {
                var result = ibookBL.RetriveBookById(BookId);
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
    }
}
