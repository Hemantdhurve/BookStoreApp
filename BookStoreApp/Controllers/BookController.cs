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

        [HttpPut]
        [Route("UpdateBook")]
        public IActionResult UpdateBookDetails(long BookId, BookModel bookModel)
        {
            try
            {
                var result = ibookBL.UpdateBookDetails(BookId, bookModel);
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

        [HttpDelete]
        [Route("DeleteBook")]
        public IActionResult DeleteBook(long BookId)
        {
            try
            {
                var result = ibookBL.DeleteBook(BookId);
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
    }
}
