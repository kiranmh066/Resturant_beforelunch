using Microsoft.EntityFrameworkCore;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantDAL.Repost
{
    public class OrderRepost:IOrderRepost
    {

        RestaurantDbContext _dbContext;//default private

        public OrderRepost(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddOrder(Order order)
        {
            _dbContext.tbl_Order.Add(order);
            _dbContext.SaveChanges();
        }

        public void DeleteOrder(int orderId)
        {
            var order = _dbContext.tbl_Order.Find(orderId);
            _dbContext.tbl_Order.Remove(order);
            _dbContext.SaveChanges();
        }

        public Order GetOrderById(int orderId)
        {
            List<Order> orders= _dbContext.tbl_Order.Include(obj => obj.Food).Include(obj => obj.HallTable).ToList();
            var result=from order in orders select order;
            foreach (var order2 in result)
            {
                if (order2.OrderId == orderId)
                {
                    return order2;
                }
                
            }
            return null;

        }

        public IEnumerable<Order> GetOrders()
        {
            return _dbContext.tbl_Order.Include(obj => obj.Food).Include(obj => obj.HallTable).ToList();
        }



        public void UpdateOrder(Order order)
        {

            _dbContext.Entry(order).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
