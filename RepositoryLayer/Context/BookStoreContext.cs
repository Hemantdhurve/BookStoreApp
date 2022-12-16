using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class BookStoreContext: DbContext
    {
        public BookStoreContext(DbContextOptions options) : base(options)

        {
        }
    }
}
