using OrderMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderMS.Infra
{
    public interface IOrderDataAccess
    {
        List<OrderModel> GetAllOrder();

        void SaveOrder(OrderModel order);
        OrderModel GetOrder(Guid orderId);
        bool DeleteOrder(Guid orderId);
    }
    public class OrderDataAccess : IOrderDataAccess
    {
        public List<OrderModel> GetAllOrder()
        {
            using (var context = new OrderDbContext())
            {
                return context.OrderData.ToList();
            }
        }
        public void SaveOrder(OrderModel order)
        {
            using (var context = new OrderDbContext())
            {
                context.Add<OrderModel>(order);
                context.SaveChanges();
            }
        }
        public OrderModel GetOrder(Guid orderId)
        {
            using (var context = new OrderDbContext())
            {
                return context.OrderData.Where(x => x.OrderId == orderId).FirstOrDefault();
            }
        }
        public bool DeleteOrder(Guid orderId)
        {
            using (var context = new OrderDbContext())
            {
                OrderModel order = context.OrderData.Where(x => x.OrderId == orderId).FirstOrDefault();

                if (order != null)
                {
                    context.Remove(order);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
