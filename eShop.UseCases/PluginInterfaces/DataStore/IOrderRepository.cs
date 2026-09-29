using eShop.CoreBusiness.Models;
using System.Collections.Generic;

namespace eShop.UseCases.PluginInterfaces.DataStore
{
    public interface IOrderRepository
    {
        Order GetOrder(int id);
        Order GetOrderByUniqueId(string uniqueId);
        int CreateOrder(Order order);
        void UpdateOrder(Order order);
        IEnumerable<Order> GetOrders();
        IEnumerable<Order> GetOutstandingOrders();
        IEnumerable<OrderLineItem> GetLineItemsByOrderId(int orderId);
    }
}
