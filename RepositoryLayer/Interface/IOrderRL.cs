using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IOrderRL
    {
        public OrderModel AddOrder(long userId, OrderModel orderModel);
        public bool DeleteOrder(long orderId);
        public IEnumerable<OrderModel> RetriveOrder(long userId);
    }
}
