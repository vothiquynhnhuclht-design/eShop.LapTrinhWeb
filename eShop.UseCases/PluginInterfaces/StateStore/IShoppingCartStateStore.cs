using System;
using System.Threading.Tasks;

namespace eShop.UseCases.PluginInterfaces.StateStore
{
    public interface IShoppingCartStateStore
    {
        void AddStateChangeListeners(Action listener);
        void RemoveStateChangeListeners(Action listener);
        void BroadcastStateChange();
        Task<int> GetLineItemsCount();
    }
}
