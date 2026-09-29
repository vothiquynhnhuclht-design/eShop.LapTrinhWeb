using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;
using System.Collections.Generic;

namespace eShop.DataStore.HardCode
{
    public class CustomerDataAccessSql : ICustomerDataAccess
    {
        public IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = 101, Name = "Le Van C (SQL)", Email = "c_sql@example.com" },
                new Customer { Id = 102, Name = "Pham Van D (SQL)", Email = "d_sql@example.com" }
            };
        }
    }
}
