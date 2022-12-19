using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IWishlistRL
    {
        public string AddWishlist(long userId, long bookId);
        public bool DeleteWishlist(long wishlistId);
        //public IEnumerable<WishlistModel> RetriveWishlist(long userId);
        public WishlistModel RetriveWishlist(long userId);
    }
}
