using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class BookBL:IBookBL
    {
        private readonly IBookRL ibookRL;
        public BookBL(IBookRL ibookRL)
        {
            this.ibookRL = ibookRL;
        }
        public BookModel AddBook(BookModel bookModel)
        {
			try
			{
				return ibookRL.AddBook(bookModel);
			}
			catch (Exception e)
			{

				throw e;
			}
        }
        public BookModel RetriveBookById(long bookId)
        {
			try
			{
				return ibookRL.RetriveBookById(bookId);
			}
			catch (Exception e)
			{

				throw e;
			}
        }
        public List<BookModel> RetriveAllBooks()
		{
			try
			{
				return ibookRL.RetriveAllBooks();

            }
			catch (Exception)
			{

				throw;
			}
		}
        public BookModel UpdateBookDetails(long bookId, BookModel bookModel)
        {
			try
			{
                return ibookRL.UpdateBookDetails(bookId, bookModel);
            }
			catch (Exception)
			{

				throw;
			}
		}
        public bool DeleteBook(long bookId)
		{
			try
			{
				return ibookRL.DeleteBook(bookId);
			}
			catch (Exception)
			{

				throw;
			}
		}


        public string ImageBooks(IFormFile image, long bookId)
        {
            try
            {
                return ibookRL.ImageBooks(image,bookId);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
