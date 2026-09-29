using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eShop.DataStore.SQL.Dapper
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ISqlDataAccess sqlDataAccess;

        public OrderRepository(ISqlDataAccess sqlDataAccess)
        {
            this.sqlDataAccess = sqlDataAccess;
        }

        public int CreateOrder(Order order)
        {
            string sqlOrder = @"INSERT INTO [dbo].[Order]
                                ([DatePlaced], [DateProcessed], [CustomerName], [CustomerAddress], [CustomerCity], [CustomerStateProvince], [CustomerCountry], [AdminUser], [UniqueId])
                                VALUES
                                (@DatePlaced, @DateProcessed, @CustomerName, @CustomerAddress, @CustomerCity, @CustomerStateProvince, @CustomerCountry, @AdminUser, @UniqueId);
                                SELECT CAST(SCOPE_IDENTITY() as int);";

            if (string.IsNullOrWhiteSpace(order.UniqueId))
            {
                order.UniqueId = Guid.NewGuid().ToString();
            }

            int orderId = sqlDataAccess.ExecuteScalar<int, object>(sqlOrder, new
            {
                order.DatePlaced,
                order.DateProcessed,
                order.CustomerName,
                order.CustomerAddress,
                order.CustomerCity,
                order.CustomerStateProvince,
                order.CustomerCountry,
                order.AdminUser,
                order.UniqueId
            });

            order.OrderId = orderId;

            if (order.LineItems != null && order.LineItems.Any())
            {
                string sqlLineItem = @"INSERT INTO [dbo].[OrderLineItem]
                                       ([ProductId], [OrderId], [Quantity], [Price])
                                       VALUES
                                       (@ProductId, @OrderId, @Quantity, @Price);";

                foreach (var lineItem in order.LineItems)
                {
                    lineItem.OrderId = orderId;
                    sqlDataAccess.ExecuteCommand(sqlLineItem, new
                    {
                        lineItem.ProductId,
                        OrderId = orderId,
                        lineItem.Quantity,
                        lineItem.Price
                    });
                }
            }

            return orderId;
        }

        public Order GetOrder(int id)
        {
            string sqlOrder = "SELECT * FROM [dbo].[Order] WHERE OrderId = @OrderId";
            var order = sqlDataAccess.LoadSingleData<Order, object>(sqlOrder, new { OrderId = id });
            if (order != null)
            {
                order.LineItems = GetLineItemsByOrderId(id).ToList();
            }
            return order;
        }

        public Order GetOrderByUniqueId(string uniqueId)
        {
            string sqlOrder = "SELECT * FROM [dbo].[Order] WHERE UniqueId = @UniqueId";
            var order = sqlDataAccess.LoadSingleData<Order, object>(sqlOrder, new { UniqueId = uniqueId });
            if (order != null && order.OrderId.HasValue)
            {
                order.LineItems = GetLineItemsByOrderId(order.OrderId.Value).ToList();
            }
            return order;
        }

        public IEnumerable<Order> GetOrders()
        {
            string sql = "SELECT * FROM [dbo].[Order]";
            var orders = sqlDataAccess.LoadData<Order, object>(sql, new { });
            foreach (var order in orders)
            {
                if (order.OrderId.HasValue)
                {
                    order.LineItems = GetLineItemsByOrderId(order.OrderId.Value).ToList();
                }
            }
            return orders;
        }

        public IEnumerable<Order> GetOutstandingOrders()
        {
            string sql = "SELECT * FROM [dbo].[Order] WHERE DateProcessed IS NULL";
            var orders = sqlDataAccess.LoadData<Order, object>(sql, new { });
            foreach (var order in orders)
            {
                if (order.OrderId.HasValue)
                {
                    order.LineItems = GetLineItemsByOrderId(order.OrderId.Value).ToList();
                }
            }
            return orders;
        }

        public IEnumerable<OrderLineItem> GetLineItemsByOrderId(int orderId)
        {
            string sql = @"SELECT li.ProductId, li.OrderId, li.Quantity, li.Price,
                                  p.Brand, p.Name, p.ImageLink, p.Description
                           FROM [dbo].[OrderLineItem] li
                           LEFT JOIN [dbo].[Product] p ON li.ProductId = p.ProductId
                           WHERE li.OrderId = @OrderId";

            var items = sqlDataAccess.LoadData<OrderLineItemDto, object>(sql, new { OrderId = orderId });
            var result = new List<OrderLineItem>();

            foreach (var item in items)
            {
                var lineItem = new OrderLineItem
                {
                    ProductId = item.ProductId,
                    OrderId = item.OrderId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Product = new Product
                    {
                        Id = item.ProductId,
                        Brand = item.Brand,
                        Name = item.Name,
                        Price = item.Price,
                        ImageLink = item.ImageLink,
                        Description = item.Description
                    }
                };
                result.Add(lineItem);
            }

            return result;
        }

        public void UpdateOrder(Order order)
        {
            string sql = @"UPDATE [dbo].[Order]
                           SET [DatePlaced] = @DatePlaced,
                               [DateProcessed] = @DateProcessed,
                               [CustomerName] = @CustomerName,
                               [CustomerAddress] = @CustomerAddress,
                               [CustomerCity] = @CustomerCity,
                               [CustomerStateProvince] = @CustomerStateProvince,
                               [CustomerCountry] = @CustomerCountry,
                               [AdminUser] = @AdminUser,
                               [UniqueId] = @UniqueId
                           WHERE OrderId = @OrderId";

            sqlDataAccess.ExecuteCommand(sql, new
            {
                order.DatePlaced,
                order.DateProcessed,
                order.CustomerName,
                order.CustomerAddress,
                order.CustomerCity,
                order.CustomerStateProvince,
                order.CustomerCountry,
                order.AdminUser,
                order.UniqueId,
                order.OrderId
            });
        }

        private class OrderLineItemDto
        {
            public int ProductId { get; set; }
            public int OrderId { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
            public string Brand { get; set; }
            public string Name { get; set; }
            public string ImageLink { get; set; }
            public string Description { get; set; }
        }
    }
}
