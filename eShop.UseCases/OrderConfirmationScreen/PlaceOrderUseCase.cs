using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;
using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;
using System;
using System.Threading.Tasks;

namespace eShop.UseCases.OrderConfirmationScreen
{
    public class PlaceOrderUseCase : IPlaceOrderUseCase
    {
        private readonly IOrderRepository orderRepository;
        private readonly IShoppingCart shoppingCart;
        private readonly IShoppingCartStateStore shoppingCartStateStore;

        public PlaceOrderUseCase(
            IOrderRepository orderRepository,
            IShoppingCart shoppingCart,
            IShoppingCartStateStore shoppingCartStateStore)
        {
            this.orderRepository = orderRepository;
            this.shoppingCart = shoppingCart;
            this.shoppingCartStateStore = shoppingCartStateStore;
        }

        public async Task<string> Execute(Order order)
        {
            if (order == null || !order.Validate())
            {
                return null;
            }

            order.DatePlaced = DateTime.Now;
            order.UniqueId = Guid.NewGuid().ToString();
            orderRepository.CreateOrder(order);

            await shoppingCart.EmptyAsync();
            shoppingCartStateStore.BroadcastStateChange();

            return order.UniqueId;
        }
    }
}
