using BusinessLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class WishlistBL:IWishlistBL
    {
        public readonly IWishlistRL iwishlistRL;
        public WishlistBL(IWishlistRL iwishlistRL)
        {
            this.iwishlistRL = iwishlistRL;
        }

        public string AddWishlist(long userId, long bookId)
        {
            try
            {
                return iwishlistRL.AddWishlist(userId, bookId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
