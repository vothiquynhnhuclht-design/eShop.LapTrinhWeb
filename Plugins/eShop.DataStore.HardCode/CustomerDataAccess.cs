using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;
using System.Collections.Generic;

namespace eShop.DataStore.HardCode
{
    public class CustomerDataAccess : ICustomerDataAccess
    {
        public IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = 1, Name = "Nguyen Van A", Email = "a@example.com" },
                new Customer { Id = 2, Name = "Tran Thi B", Email = "b@example.com" }
            };
        }
    }
}
