using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICartRL
    {
        public CartModel AddCart(CartModel cartModel, long UserId);
        public IEnumerable<CartModel> RetriveCart(long UserId);
    }
}
