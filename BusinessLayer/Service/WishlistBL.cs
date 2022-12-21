using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
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

        public bool DeleteWishlist(long wishlistId)
        {
            try
            {
                return iwishlistRL.DeleteWishlist(wishlistId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<WishlistModel> RetriveWishlist(long userId)
        {
            try
            {
                return iwishlistRL.RetriveWishlist(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
