using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
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
    }
}
