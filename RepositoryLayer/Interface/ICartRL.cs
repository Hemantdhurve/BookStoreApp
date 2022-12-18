using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICartRL
    {
        public CartModel AddCart(CartModel cartModel, long userId);
        public IEnumerable<CartModel> RetriveCart(long userId);
        public string UpdateCartQty(long cartId, long bookQuantity);
        public bool DeleteCart(long cartId);
    }
}
