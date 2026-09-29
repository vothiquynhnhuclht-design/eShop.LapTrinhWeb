using eShop.UseCases.PluginInterfaces.DataStore;
using System;

namespace eShop.UseCases.AdminPortal.OrderDetailScreen
{
    public class ProcessOrderUseCase : IProcessOrderUseCase
    {
        private readonly IOrderRepository orderRepository;

        public ProcessOrderUseCase(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public bool Execute(int orderId, string adminUserName)
        {
            var order = orderRepository.GetOrder(orderId);
            if (order != null)
            {
                order.DateProcessed = DateTime.Now;
                order.AdminUser = adminUserName;
                orderRepository.UpdateOrder(order);
                return true;
            }
            return false;
        }
    }
}
