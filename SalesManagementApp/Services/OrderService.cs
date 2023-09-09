using SalesManagementApp.Data;
using SalesManagementApp.Entities;
using SalesManagementApp.Models;
using SalesManagementApp.Services.Contracts;

namespace SalesManagementApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly SalesManagementDbContext salesManagementDbContext;

        public OrderService(SalesManagementDbContext salesManagementDbContext)
        {
            this.salesManagementDbContext=salesManagementDbContext;
        }
        public async Task CreateOrder(OrderModel orderModel)
        {
            try
            {
                Order order = new Order
                {
                    OrderDateTime = DateTime.Now,
                    ClientId = orderModel.ClientId,
                    EmployeeId = 9,
                    Price = orderModel.OrderItems.Sum(o=>o.Price),
                    Qty = orderModel.OrderItems.Sum(o=>o.Qty)

                };

                var addedOrder = await this.salesManagementDbContext.Orders.AddAsync(order);
                await this.salesManagementDbContext.SaveChangesAsync();
                int orderId = addedOrder.Entity.Id;

                var orderItemsToAdd = ReturnOrderItemsWithOrderId(orderId, orderModel.OrderItems);
                this.salesManagementDbContext.AddRange(orderItemsToAdd);
                await this.salesManagementDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private List<OrderItem>ReturnOrderItemsWithOrderId( int orderId, List<OrderItem> orderItems)
        {
            return (from oi in orderItems
                    select new OrderItem
                    {
                        OrderId = orderId,
                        Price = oi.Price,
                        Qty = oi.Qty,
                        ProductId = oi.ProductId
                    }).ToList();
        }
    }
}
