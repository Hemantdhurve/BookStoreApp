using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IWishlistBL
    {
        public string AddWishlist(long userId, long bookId);
        public bool DeleteWishlist(long wishlistId);
        public IEnumerable<WishlistModel> RetriveWishlist(long userId);

    }
}
