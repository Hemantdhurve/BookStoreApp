using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IBookBL
    {
        public BookModel AddBook(BookModel bookModel);
        public BookModel RetriveBookById(long bookId);
        public List<BookModel> RetriveAllBooks();
        public BookModel UpdateBookDetails(long bookId, BookModel bookModel);
        public bool DeleteBook(long bookId);
        public string ImageBooks(IFormFile image, long bookId);
    }
}
