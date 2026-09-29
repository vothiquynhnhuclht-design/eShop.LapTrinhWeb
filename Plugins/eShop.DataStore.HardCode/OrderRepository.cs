using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;
using System.Collections.Generic;
using System.Linq;

namespace eShop.DataStore.HardCode
{
    public class OrderRepository : IOrderRepository
    {
        private Dictionary<int, Order> orders;

        public OrderRepository()
        {
            orders = new Dictionary<int, Order>();

            var order1 = new Order
            {
                OrderId = 1,
                UniqueId = System.Guid.NewGuid().ToString(),
                DatePlaced = System.DateTime.Now.AddHours(-2),
                CustomerName = "John Doe",
                CustomerAddress = "123 Main Street",
                CustomerCity = "Toronto",
                CustomerStateProvince = "ON",
                CustomerCountry = "Canada",
                LineItems = new List<OrderLineItem>
                {
                    new OrderLineItem { ProductId = 495, Price = 14.99, Quantity = 2, Product = new Product { Id = 495, Brand = "maybelline", Name = "Maybelline Face Studio Master Hi-Light Light Booster Bronzer", Price = 14.99, ImageLink = "https://d3t32hsnjxo7q6.cloudfront.net/i/991799d3e70b8856686979f8ff6dcfe0_ra,w158,h184_pa,w158,h184.png" } },
                    new OrderLineItem { ProductId = 488, Price = 10.29, Quantity = 1, Product = new Product { Id = 488, Brand = "maybelline", Name = "Maybelline Fit Me Bronzer", Price = 10.29, ImageLink = "https://d3t32hsnjxo7q6.cloudfront.net/i/d4f7d82b4858c622bb3c1cef07b9d850_ra,w158,h184_pa,w158,h184.png" } }
                }
            };
            orders.Add(1, order1);

            var order2 = new Order
            {
                OrderId = 2,
                UniqueId = System.Guid.NewGuid().ToString(),
                DatePlaced = System.DateTime.Now.AddHours(-5),
                CustomerName = "Jane Smith",
                CustomerAddress = "456 Queen West",
                CustomerCity = "Vancouver",
                CustomerStateProvince = "BC",
                CustomerCountry = "Canada",
                LineItems = new List<OrderLineItem>
                {
                    new OrderLineItem { ProductId = 477, Price = 15.99, Quantity = 1, Product = new Product { Id = 477, Brand = "maybelline", Name = "Maybelline Facestudio Master Contour Kit", Price = 15.99, ImageLink = "https://d3t32hsnjxo7q6.cloudfront.net/i/4f731de249cbd4cb819ea7f5f4cfb5c3_ra,w158,h184_pa,w158,h184.png" } }
                }
            };
            orders.Add(2, order2);
        }

        public int CreateOrder(Order order)
        {
            order.OrderId = orders.Count + 1;
            if (string.IsNullOrWhiteSpace(order.UniqueId))
            {
                order.UniqueId = System.Guid.NewGuid().ToString();
            }
            orders.Add(order.OrderId.Value, order);
            return order.OrderId.Value;
        }

        public Order GetOrder(int id)
        {
            if (orders.ContainsKey(id))
            {
                return orders[id];
            }
            return null;
        }

        public Order GetOrderByUniqueId(string uniqueId)
        {
            return orders.Values.FirstOrDefault(x => x.UniqueId == uniqueId);
        }

        public void UpdateOrder(Order order)
        {
            if (order != null && order.OrderId.HasValue && orders.ContainsKey(order.OrderId.Value))
            {
                orders[order.OrderId.Value] = order;
            }
        }

        public IEnumerable<Order> GetOrders()
        {
            return orders.Values;
        }

        public IEnumerable<Order> GetOutstandingOrders()
        {
            return orders.Values.Where(x => !x.DateProcessed.HasValue);
        }

        public IEnumerable<OrderLineItem> GetLineItemsByOrderId(int orderId)
        {
            if (orders.ContainsKey(orderId))
            {
                return orders[orderId].LineItems;
            }
            return new List<OrderLineItem>();
        }
    }
}
