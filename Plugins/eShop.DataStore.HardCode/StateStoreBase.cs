using System;

namespace eShop.DataStore.HardCode
{
    public class StateStoreBase
    {
        protected Action listeners;

        public void AddStateChangeListeners(Action listener)
        {
            listeners += listener;
        }

        public void RemoveStateChangeListeners(Action listener)
        {
            listeners -= listener;
        }

        public void BroadcastStateChange()
        {
            if (listeners != null)
            {
                listeners.Invoke();
            }
        }
    }
}
