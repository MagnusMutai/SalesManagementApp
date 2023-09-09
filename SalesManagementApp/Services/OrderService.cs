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
                    OrderDateTime = orderModel.OrderDateTime,
                    ClientId = orderModel.ClientId,
                    EmployeeId = 9,
                    Price = orderModel.Price,
                    Qty = orderModel.Qty

                };

                var addedOrder = await this.salesManagementDbContext.Orders.AddAsync(order);
                int orderId = addedOrder.Entity.Id;

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
