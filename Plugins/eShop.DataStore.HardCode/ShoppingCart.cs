using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.UI;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.DataStore.HardCode
{
    public class ShoppingCart : IShoppingCart
    {
        private Order order;

        public ShoppingCart()
        {
            order = new Order();
        }

        public Task<Order> GetOrderAsync()
        {
            return Task.FromResult(order);
        }

        public Task<Order> AddProductAsync(Product product)
        {
            if (product != null)
            {
                order.AddProduct(product.Id, 1, product.Price);
                var item = order.LineItems.FirstOrDefault(x => x.ProductId == product.Id);
                if (item != null)
                {
                    item.Product = product;
                }
            }
            return Task.FromResult(order);
        }

        public Task<Order> UpdateQuantityAsync(int productId, int quantity)
        {
            var item = order.LineItems.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;
            }
            return Task.FromResult(order);
        }

        public Task<Order> UpdateOrderAsync(Order order)
        {
            this.order = order;
            return Task.FromResult(this.order);
        }

        public Task<Order> DeleteProductAsync(int productId)
        {
            order.RemoveProduct(productId);
            return Task.FromResult(order);
        }

        public Task<Order> PlaceOrderAsync()
        {
            order = new Order();
            return Task.FromResult(order);
        }

        public Task EmptyAsync()
        {
            order.LineItems.Clear();
            return Task.CompletedTask;
        }
    }
}
