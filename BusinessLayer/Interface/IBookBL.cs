﻿using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IBookBL
    {
        public BookModel AddBook(BookModel bookModel);
        public BookModel RetriveBookById(long BookId);
        public List<BookModel> RetriveAllBooks();
        public BookModel UpdateBookDetails(long BookId, BookModel bookModel);
        public bool DeleteBook(long BookId);
    }
}
