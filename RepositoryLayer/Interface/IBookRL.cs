using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IBookRL
    {
        public BookModel AddBook(BookModel bookModel);
        public BookModel RetriveBookById(long BookId);
        public List<BookModel> RetriveAllBooks();
        public BookModel UpdateBookDetails(long BookId, BookModel bookModel);
        public bool DeleteBook(long BookId);
    }
}
