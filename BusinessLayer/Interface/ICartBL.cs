using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICartBL
    {
        public CartModel AddCart(CartModel cartModel, long userId);
        public IEnumerable<CartModel> RetriveCart(long userId);
        public string UpdateCartQty(long cartId, long bookQuantity);
        public bool DeleteCart(long cartId);
    }
}
