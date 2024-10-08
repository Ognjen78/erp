﻿using ErpProject.Controllers;
using ErpProject.Interface;
using ErpProject.Models;

namespace ErpProject.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext dbContext;
        public OrderRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Order> addOrder(Order order)
        {
            dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();
            return order;
        }

        public void deleteOrder(int id)
        {
            var order = dbContext.Orders.Find(id);
            if (order != null)
            {
                dbContext.Orders.Remove(order);
                dbContext.SaveChanges();
            }
        }

        public List<Order> getAllOrders()
        {
            return dbContext.Orders.ToList();
        }

        public Order getOrderById(int id)
        {
            return dbContext.Orders.Find(id);
        }

        public Order updateOrder(Order order)
        {
            var o  = dbContext.Orders.Find(order.id_order);
            if (o != null)
            {
                o.order_date = order.order_date;
                o.items_price = order.items_price;
                o.total_price = order.total_price;
                o.id_transaction = order.id_transaction;
                o.transaction_date = order.transaction_date;
                o.transaction_amount = order.transaction_amount;
                o.id_user = order.id_user;
                o.id_shipping = order.id_shipping;
                dbContext.SaveChanges();
            }
            return order;
        }
    }
}
