using BusinessLayer.Interface;
using CommonLayer.Model;
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
			catch (Exception)
			{

				throw;
			}
        }
    }
}
