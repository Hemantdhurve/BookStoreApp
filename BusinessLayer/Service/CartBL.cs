using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CartBL:ICartBL
    {
        private readonly ICartRL icartRL;

        public CartBL(ICartRL icartRL)
		{
			this.icartRL = icartRL;				
		}
        public CartModel AddCart(CartModel cartModel, long userId)
        {
			try
			{
				return icartRL.AddCart(cartModel, userId);
			}
			catch (Exception)
			{

				throw;
			}
        }
        public IEnumerable<CartModel> RetriveCart(long userId)
		{
			try
			{
                return icartRL.RetriveCart(userId);
            }
			catch (Exception)
			{

				throw;
			}
		}

        public string UpdateCartQty(long cartId, long bookQuantity)
		{
			try
			{
                return icartRL.UpdateCartQty(cartId, bookQuantity);
            }
			catch (Exception)
			{

				throw;
			}
		}
    }
}
