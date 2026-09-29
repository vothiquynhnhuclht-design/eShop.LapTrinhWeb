using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.DataStore.HardCode
{
    public class ShoppingCartStateStore : StateStoreBase, IShoppingCartStateStore
    {
        private readonly IShoppingCart shoppingCart;

        public ShoppingCartStateStore(IShoppingCart shoppingCart)
        {
            this.shoppingCart = shoppingCart;
        }

        public async Task<int> GetLineItemsCount()
        {
            var order = await shoppingCart.GetOrderAsync();
            return order?.LineItems?.Sum(x => x.Quantity) ?? 0;
        }
    }
}
