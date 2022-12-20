using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class OrderBL:IOrderBL
    {
        public readonly IOrderRL iorderRL;

        public OrderBL(IOrderRL iorderRL)
        {
            this.iorderRL = iorderRL;           
        }
        public OrderModel AddOrder(long userId, OrderModel orderModel)
        {
            try
            {
                return iorderRL.AddOrder(userId, orderModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
